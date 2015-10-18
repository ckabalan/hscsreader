// <copyright file="Uploader.cs" company="SpectralCoding.com">
//     Copyright (c) 2015 SpectralCoding
// </copyright>
// <license>
// This file is part of HSCSReader.
// 
// HSCSReader is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// HSCSReader is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with HSCSReader.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <author>Caesar Kabalan</author>

using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using HSCSReader.Replay;
using NLog;

namespace HSCSReader.DataStorage {
	public static class Uploader {
		private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
		private static ISession _session;

		/// <summary>
		/// Creates a session with the Cassandra server.
		/// </summary>
		private static void CreateSession() {
			if (_session == null) {
				Cluster cluster = Cluster.Builder().AddContactPoint("192.168.1.197").Build();
				_session = cluster.Connect("hscs");
			}
		}

		/// <summary>
		/// Compiles and commits the metrics for a specific replay.
		/// </summary>
		/// <param name="metrics">A Dictionary containing a list of Metrics by card.</param>
		public static void UploadReplay(Dictionary<String, List<Metric>> metrics) {
			CreateSession();
			foreach (KeyValuePair<String, List<Metric>> metricKVP in metrics) {
				SubmitCardMetrics(metricKVP.Key, metricKVP.Value);
			}
		}

		/// <summary>
		/// Commit metrics for a specific card.
		/// </summary>
		/// <param name="cardId">The ID of the card to commit metrics for.</param>
		/// <param name="metrics">The list of metrics to commit.</param>
		private static void SubmitCardMetrics(String cardId, List<Metric> metrics) {
			CreateSession();
			Row result = _session.Execute("SELECT * FROM cards WHERE cardid='" + cardId + "'").FirstOrDefault();
			if (result == null) {
				PreparedStatement statement = _session.Prepare("INSERT INTO cards (cardid) VALUES (:CARDID)");
				_session.Execute(statement.Bind(new {CARDID = cardId}));
				result = _session.Execute("SELECT * FROM cards WHERE cardid='" + cardId + "'").FirstOrDefault();
			}
			foreach (Metric curMetric in metrics) {
				String[] nameValSplit = curMetric.Name.Split('.');
				String[] nameSplit = nameValSplit[0].Split('_');
				String metricName = nameValSplit[0];
				Int32 metricSubIndex = -1;
				if (nameValSplit.Length == 2) { metricSubIndex = Convert.ToInt32(nameValSplit[1]); }
				String metricType = nameSplit[0];
				if (metricType == "COUNT") {
					if (nameValSplit.Length == 2) {
						// For updating map counters
						// Example: Zone Changes on Turn
						SortedDictionary<Int32, Int32> existingMap = (SortedDictionary<Int32, Int32>)result?[metricName];
						Int32 oldValue = 0;
						if ((existingMap != null) && (existingMap.ContainsKey(metricSubIndex))) {
							oldValue = existingMap[metricSubIndex];
						}
						String cql = $"UPDATE cards SET \"{metricName}\"[{nameValSplit[1]}] = :NEWVALUE WHERE cardid = :CARDID";
						PreparedStatement statement = _session.Prepare(cql);
						_session.Execute(statement.Bind(new {CARDID = cardId, NEWVALUE = (oldValue + curMetric.Values[0])}));
						Logger.Debug(
									 $"Updating {cardId}: {metricName}[{nameValSplit[1]}] {oldValue} -> {(oldValue + curMetric.Values[0])}");
					} else {
						// For updating non-map counters
						// Example: Total Card Mulligan appearances
						Int32 oldValue = 0;
						if (result?[metricName] != null) {
							oldValue = Convert.ToInt32(result?[metricName]);
						}
						String cql = $"UPDATE cards SET \"{metricName}\" = :NEWVALUE WHERE cardid = :CARDID";
						PreparedStatement statement = _session.Prepare(cql);
						_session.Execute(statement.Bind(new { CARDID = cardId, NEWVALUE = (oldValue + curMetric.Values[0]) }));
						Logger.Debug(
									 $"Updating {cardId}: {metricName} {oldValue} -> {(oldValue + curMetric.Values[0])}");
					}
				} else if (metricType == "COUNTGAME") {
					// For game counters 
					// Example: Card Drew X Cards in Game
					SortedDictionary<Int32, Int32> existingMap = (SortedDictionary<Int32, Int32>)result?[metricName];
					Int32 oldValue = 0;
					if ((existingMap != null) && (existingMap.ContainsKey(curMetric.Values[0]))) {
						oldValue = existingMap[curMetric.Values[0]];
					}
					String cql = $"UPDATE cards SET \"{metricName}\"[{curMetric.Values[0]}] = :NEWVALUE WHERE cardid = :CARDID";
					PreparedStatement statement = _session.Prepare(cql);
					_session.Execute(statement.Bind(new { CARDID = cardId, NEWVALUE = (oldValue + 1) }));
					Logger.Debug(
								 $"Updating {cardId}: {metricName}[{curMetric.Values[0]}] {oldValue} -> {(oldValue + 1)}");
				} else if (metricType == "MAX") {
					// For high scores
					if (Convert.ToInt32(result?[metricName]) < curMetric.Values[0]) {
						String cql = $"UPDATE cards SET \"{metricName}\" = :NEWVALUE WHERE cardid = :CARDID";
						PreparedStatement statement = _session.Prepare(cql);
						_session.Execute(statement.Bind(new {CARDID = cardId, NEWVALUE = curMetric.Values[0]}));
						Logger.Debug($"Updating {cardId}: {metricName} {result?[metricName]} -> {curMetric.Values[0]}");
					}
				}
			}
			//Console.WriteLine("{0} {1}", result["firstname"], result["age"]);
		}

		/// <summary>
		/// Marks the list of games as having been consumed and commited by the application.
		/// </summary>
		/// <param name="games">A list of Game objects to commit.</param>
		public static void MarkGamesConsumed(List<Game> games) {
			CreateSession();
			String cql = "INSERT INTO games (id, filename, submitted, xmlmd5) VALUES (:ID, :FILENAME, :SUBMITTED, :XMLMD5)";
			foreach (Game curGame in games) {
				if (curGame.IsNewGame) {
					PreparedStatement statement = _session.Prepare(cql);
					// TODO: Get Filename of Game.
					_session.Execute(
									 statement.Bind(
													 new {
															ID = Guid.NewGuid(),
															FILENAME = "",
															SUBMITTED = new DateTimeOffset(DateTime.Now),
															XMLMD5 = curGame.Md5Hash
														}));
					Logger.Debug($"Marking Game Consumed: {curGame.Md5Hash}");
				}
			}
		}

		/// <summary>
		/// Checks if the game has already been fully analyzed and commited by the application.
		/// </summary>
		/// <param name="md5String">The md5 string to check.</param>
		/// <returns>A Boolean indicating whether or not a game has not been previously seen.</returns>
		public static Boolean IsNewGame(String md5String) {
			CreateSession();
			Logger.Trace("Checking if MD5 Exists in Games table...");
			PreparedStatement statement = _session.Prepare("SELECT * FROM games WHERE xmlmd5=:MD5STR");
			Row result = _session.Execute(statement.Bind(new {MD5STR = md5String})).FirstOrDefault();
			if (result == null) {
				// No existing game with this MD5 Hash so assume it is new.
				return true;
			}
			// We found a matching hash, so return false.
			return false;
		}
	}
}