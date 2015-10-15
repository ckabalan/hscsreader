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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using HSCSReader.Replay;
using NLog;
using Logger = Cassandra.Logger;

namespace HSCSReader.DataStorage {
	public static class Uploader {
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
		private static ISession session;

		/// <summary>
		/// Creates a session with the Cassandra server.
		/// </summary>
		private static void CreateSession() {
			if (session == null) {
				Cluster cluster = Cluster.Builder().AddContactPoint("192.168.1.197").Build();
				session = cluster.Connect("hscs");
			}
		}

		/// <summary>
		/// Compiles and commits the metrics for a specific replay.
		/// </summary>
		/// <param name="metrics">A Dictionary containing a list of Metrics by card.</param>
		public static void UploadReplay(Dictionary<string, List<Metric>> metrics) {
			CreateSession();
			foreach (KeyValuePair<string, List<Metric>> metricKVP in metrics) {
				SubmitCardMetrics(metricKVP.Key, metricKVP.Value);
			}
		}

		/// <summary>
		/// Commit metrics for a specific card.
		/// </summary>
		/// <param name="cardID">The ID of the card to commit metrics for.</param>
		/// <param name="metrics">The list of metrics to commit.</param>
		private static void SubmitCardMetrics(string cardID, List<Metric> metrics) {
			CreateSession();
			Row result = session.Execute("SELECT * FROM cards WHERE cardid='" + cardID + "'").FirstOrDefault();
			if (result == null) {
				PreparedStatement statement = session.Prepare("INSERT INTO cards (cardid) VALUES (:CARDID)");
				session.Execute(statement.Bind(new {CARDID = cardID}));
				result = session.Execute("SELECT * FROM cards WHERE cardid='" + cardID + "'").FirstOrDefault();
			}
			foreach (Metric curMetric in metrics) {
				string[] nameValSplit = curMetric.Name.Split('.');
				string[] nameSplit = nameValSplit[0].Split('_');
				string metricName = nameValSplit[0];
				int metricSubIndex = -1;
				if (nameValSplit.Length == 2) { metricSubIndex = Convert.ToInt32(nameValSplit[1]); }
				string metricType = nameSplit[0];
				if (metricType == "COUNT") {
					if (nameValSplit.Length == 2) {
						SortedDictionary<int, int> existingMap = (SortedDictionary<int, int>)result[metricName];
						int oldValue = 0;
						if ((existingMap != null) && (existingMap.ContainsKey(metricSubIndex))) {
							oldValue = existingMap[metricSubIndex];
						}
						string cql = $"UPDATE cards SET \"{metricName}\"[{nameValSplit[1]}] = :NEWVALUE WHERE cardid = :CARDID";
						PreparedStatement statement = session.Prepare(cql);
						session.Execute(statement.Bind(new {CARDID = cardID, NEWVALUE = (oldValue + curMetric.Values[0])}));
						logger.Debug(
									 $"Updating {cardID}: {metricName}[{nameValSplit[1]}] {oldValue} -> {(oldValue + curMetric.Values[0])}");
					}
				} else if (metricType == "MAX") {
					if (Convert.ToInt32(result[metricName]) < curMetric.Values[0]) {
						string cql = $"UPDATE cards SET \"{metricName}\" = :NEWVALUE WHERE cardid = :CARDID";
						PreparedStatement statement = session.Prepare(cql);
						session.Execute(statement.Bind(new {CARDID = cardID, NEWVALUE = curMetric.Values[0]}));
						logger.Debug($"Updating {cardID}: {metricName} {result[metricName]} -> {curMetric.Values[0]}");
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
			string cql = $"INSERT INTO games (id, filename, submitted, xmlmd5) VALUES (:ID, :FILENAME, :SUBMITTED, :XMLMD5)";
			foreach (Game curGame in games) {
				if (curGame.IsNewGame) {
					PreparedStatement statement = session.Prepare(cql);
					// TODO: Get Filename of Game.
					session.Execute(
									 statement.Bind(
													 new {
															ID = Guid.NewGuid(),
															FILENAME = "",
															SUBMITTED = new DateTimeOffset(DateTime.Now),
															XMLMD5 = curGame.Md5Hash
														}));
					logger.Debug($"Marking Game Consumed: {curGame.Md5Hash}");
				}
			}
		}

		/// <summary>
		/// Checks if the game has already been fully analyzed and commited by the application.
		/// </summary>
		/// <param name="md5String">The md5 string to check.</param>
		/// <returns>A Boolean indicating whether or not a game has not been previously seen.</returns>
		public static bool IsNewGame(string md5String) {
			CreateSession();
			logger.Trace($"Checking if MD5 Exists in Games table...");
			PreparedStatement statement = session.Prepare("SELECT * FROM games WHERE xmlmd5=:MD5STR");
			Row result = session.Execute(statement.Bind(new {MD5STR = md5String})).FirstOrDefault();
			if (result == null) {
				// No existing game with this MD5 Hash so assume it is new.
				return true;
			}
			// We found a matching hash, so return false.
			return false;
		}
	}
}