// <copyright file="HSReplay.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.DataStorage;
using HSCSReader.Replay.EntityStates;
using HSCSReader.Support;
using HSCSReader.Support.CardDefinitions;
using NLog;

namespace HSCSReader.Replay {
	public class HSReplay {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly String _filePath;
		private readonly List<Game> _games = new List<Game>();
		private Double _build;
		private Double _version;

		/// <summary>
		/// Initializes an instance of the HSReplay class.
		/// </summary>
		/// <param name="filePath">Path of the HSReplay XML file.</param>
		public HSReplay(String filePath) {
			_filePath = filePath;
			if (Validate()) {
				Parse();
				Dictionary<String, List<Metric>> collapsedMetrics = CollapseMetrics();
				//PrintMetrics(collapsedMetrics);
				Uploader.UploadReplay(collapsedMetrics);
				Uploader.MarkGamesConsumed(_games);
			}
		}

		/// <summary>
		/// Parses the HSReplay XML file.
		/// </summary>
		private void Parse() {
			XmlDocument replayDoc = new XmlDocument();
			replayDoc.Load(_filePath);

			XmlNode rootNode = replayDoc.SelectSingleNode("/HSReplay");
			Double.TryParse(rootNode?.Attributes?["version"]?.Value, out _version);
			Double.TryParse(rootNode?.Attributes?["build"]?.Value, out _build);

			XmlNodeList gameNodeList = replayDoc.SelectNodes("/HSReplay/Game");
			if (gameNodeList == null) { return; }
			foreach (XmlNode curGame in gameNodeList) {
				_games.Add(new Game(curGame));
				Logger.Info("Game Complete.");
				//return;
			}
		}

		/// <summary>
		/// Returns a list of metrics by card.
		/// </summary>
		/// <returns>A Dictionary containing a list of Metrics by card.</returns>
		private Dictionary<String, List<Metric>> CollapseMetrics() {
			Dictionary<String, List<Metric>> metrics = new Dictionary<String, List<Metric>>();
			foreach (Game curGame in _games) {
				if (curGame.IsNewGame) {
					foreach (KeyValuePair<Int32, EntityState> entityKVP in curGame.ActorStates) {
						if (entityKVP.Value.GetType() == typeof(FullEntityState)) {
							FullEntityState curEntity = (FullEntityState)entityKVP.Value;
							if (curEntity.CardId != null) {
								if (!metrics.ContainsKey(curEntity.CardId)) {
									metrics.Add(curEntity.CardId, new List<Metric>());
								}
								Helpers.IntegrateMetrics(curEntity.Metrics, metrics[curEntity.CardId]);
							}
						}
					}
				}
			}
			return metrics;
		}

		/// <summary>
		/// Print all the metrics from this replay.
		/// </summary>
		/// <param name="metrics">A Dictionary containing a list of Metrics by card.</param>
		public void PrintMetrics(Dictionary<String, List<Metric>> metrics) {
			foreach (KeyValuePair<String, List<Metric>> curCardId in metrics) {
				Logger.Debug("Entity: " + CardDefs.Cards[curCardId.Key].ShortDescription);
				foreach (Metric curMetric in curCardId.Value) {
					Logger.Debug("\t{0} = {1}", curMetric.Name, String.Join(",", curMetric.Values));
				}
			}
		}

		/// <summary>
		/// Validates a HSReplay file is in a valid format.
		/// </summary>
		/// <returns>A Boolean indicating whether or not the file is valid.</returns>
		private Boolean Validate() {
			// TODO: Fix this so that validation actually does something. Maybe after jleclanche finishes the XML spec and DTD.
			//// Set the validation settings.
			//XmlReaderSettings validationSettings = new XmlReaderSettings();
			//validationSettings.DtdProcessing = DtdProcessing.Parse;
			//validationSettings.ValidationType = ValidationType.DTD;
			//validationSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
			//
			//// Create the XmlReader object.
			//XmlReader reader = XmlReader.Create(_filePath, validationSettings);
			//
			//// Parse the file. 
			//while (reader.Read());
			//Console.WriteLine("Validation finished");
			return true;
		}

		//private static void ValidationCallBack(object sender, ValidationEventArgs e) {
		//	Console.WriteLine("Validation Error: {0}", e.Message);

		//}
	}
}