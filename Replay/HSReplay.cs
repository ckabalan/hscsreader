﻿// <copyright file="HSReplay.cs" company="SpectralCoding.com">
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
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.DataStorage;
using HSCSReader.Support;
using HSCSReader.Support.CardDefinitions;
using NLog;

namespace HSCSReader.Replay {
	public class HSReplay {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private readonly string _filePath;
		private double _build;
		private List<Game> _games = new List<Game>();
		private double _version;

		/// <summary>
		/// Initializes an instance of the HSReplay class.
		/// </summary>
		/// <param name="filePath">Path of the HSReplay XML file.</param>
		public HSReplay(string filePath) {
			_filePath = filePath;
			if (Validate()) {
				Parse();
				//Dictionary<String, List<Metric>> collapsedMetrics = CollapseMetrics();
				//PrintMetrics(collapsedMetrics);
				//Uploader.UploadReplay(collapsedMetrics);
				//Uploader.MarkGamesConsumed(_games);
			}
		}

		/// <summary>
		/// Parses the HSReplay XML file.
		/// </summary>
		private void Parse() {
			XmlDocument replayDoc = new XmlDocument();
			replayDoc.Load(_filePath);

			XmlNode rootNode = replayDoc.SelectSingleNode("/HSReplay");
			double.TryParse(rootNode?.Attributes?["version"]?.Value, out _version);
			double.TryParse(rootNode?.Attributes?["build"]?.Value, out _build);

			XmlNodeList gameNodeList = replayDoc.SelectNodes("/HSReplay/Game");
			foreach (XmlNode curGame in gameNodeList) {
				_games.Add(new Game(curGame));
				logger.Info("Game Complete.");
				return;
			}
		}

		/// <summary>
		/// Validates a HSReplay file is in a valid format.
		/// </summary>
		/// <returns>A Boolean indicating whether or not the file is valid.</returns>
		private bool Validate() {
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

		//}
		//	Console.WriteLine("Validation Error: {0}", e.Message);

		//private static void ValidationCallBack(object sender, ValidationEventArgs e) {
	}
}