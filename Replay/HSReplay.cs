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
				Console.WriteLine();
				Dictionary<String, Dictionary<String, Int32>> collapsedMetrics = CollapseMetrics();
				PrintMetrics(collapsedMetrics);
			}
		}

		public Dictionary<String, Dictionary<String, Int32>> CollapseMetrics() {
			Dictionary<String, Dictionary<String, Int32>> metrics = new Dictionary<String, Dictionary<String, Int32>>();
			foreach (Game curGame in _games) {
				foreach (KeyValuePair<Int32, Entity> entityKVP in curGame.Entities) {
					Entity curEntity = entityKVP.Value;
					if (curEntity.Attributes.ContainsKey("cardID")) {
						foreach (KeyValuePair<String, Int32> metricKVP in curEntity.Metrics) {
							if (!metrics.ContainsKey(curEntity.Attributes["cardID"])) {
								metrics.Add(curEntity.Attributes["cardID"], new Dictionary<String, Int32>());
							}
							if (!metrics[curEntity.Attributes["cardID"]].ContainsKey(metricKVP.Key)) {
								metrics[curEntity.Attributes["cardID"]].Add(metricKVP.Key, 0);
                            }
							metrics[curEntity.Attributes["cardID"]][metricKVP.Key] += metricKVP.Value;
						}
					}
				}
			}
			return metrics;
		}

		public void PrintMetrics(Dictionary<String, Dictionary<String, Int32>> metrics) {
			foreach (KeyValuePair<String, Dictionary<String, Int32>> curCardID in metrics) {
				Console.WriteLine($"Card {curCardID.Key}");
				foreach (KeyValuePair<String, Int32> metricKVP in curCardID.Value) {
					Console.WriteLine($"\t{metricKVP.Key} = {metricKVP.Value}");
				}
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
				//return;
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
