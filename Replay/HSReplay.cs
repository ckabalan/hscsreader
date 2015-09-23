using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	class HSReplay {
		private readonly String _filePath;
		private double _version;
		private List<Game> _games = new List<Game>(); 

		public HSReplay(String filePath) {
			_filePath = filePath;
			if (Validate()) {
				Parse();
			}
		}

		private void Parse() {
			XmlDocument replayDoc = new XmlDocument();
			replayDoc.Load(_filePath);

			XmlNode rootNode = replayDoc.SelectSingleNode("/HSReplay");
			Double.TryParse(rootNode.Attributes["version"].Value, out _version);

			XmlNodeList gameNodeList = replayDoc.SelectNodes("/HSReplay/Game");
			foreach (XmlNode curGame in gameNodeList) {
				_games.Add(new Game(curGame));
				Console.Write(".");
				return;
			}
		}

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
