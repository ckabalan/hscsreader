using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support;
using HSCSReader.Support.CardDefinitions;

namespace HSCSReader.Replay {
	public class HSReplay {
		private readonly String _filePath;
		private double _version;
		private List<Game> _games = new List<Game>(); 

		public HSReplay(String filePath) {
			_filePath = filePath;
			if (Validate()) {
				Parse();
				Console.WriteLine();
				Dictionary<String, List<Metric>> collapsedMetrics = CollapseMetrics();
				PrintMetrics(collapsedMetrics);
			}
		}

		public Dictionary<String, List<Metric>> CollapseMetrics() {
			Dictionary<String, List<Metric>> metrics = new Dictionary<String, List<Metric>>();
			foreach (Game curGame in _games) {
				foreach (KeyValuePair<Int32, Entity> entityKVP in curGame.Entities) {
					Entity curEntity = entityKVP.Value;
					if (curEntity.Attributes.ContainsKey("cardID")) {
						if (!metrics.ContainsKey(curEntity.Attributes["cardID"])) {
                            metrics.Add(curEntity.Attributes["cardID"], new List<Metric>());
						}
						Helpers.IntegrateMetrics(entityKVP.Value.Metrics, metrics[curEntity.Attributes["cardID"]], false);
					}
				}
			}
			return metrics;
		}

		public void PrintMetrics(Dictionary<String, List<Metric>> metrics) {
			foreach (KeyValuePair<String, List<Metric>> curCardID in metrics) {
				Console.WriteLine("Entity: " + CardDefs.Cards[curCardID.Key].ShortDescription);
				foreach (Metric curMetric in curCardID.Value) {
					Console.WriteLine("\t{0} = {1}", curMetric.Name, String.Join(",", curMetric.Values));
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
