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
		private readonly String _filePath;
		private double _version;
		private List<Game> _games = new List<Game>(); 

		/// <summary>
		/// Initializes an instance of the HSReplay class.
		/// </summary>
		/// <param name="filePath">Path of the HSReplay XML file.</param>
		public HSReplay(String filePath) {
			_filePath = filePath;
			if (Validate()) {
				Parse();
				Dictionary<String, List<Metric>> collapsedMetrics = CollapseMetrics();
				PrintMetrics(collapsedMetrics);
				Uploader.UploadReplay(collapsedMetrics);
				Uploader.MarkGamesConsumed(_games);
			}
		}

		/// <summary>
		/// Returns a list of metrics by card.
		/// </summary>
		/// <returns>A Dictionary containing a list of Metrics by card.</returns>
		public Dictionary<String, List<Metric>> CollapseMetrics() {
			Dictionary<String, List<Metric>> metrics = new Dictionary<String, List<Metric>>();
			foreach (Game curGame in _games) {
				if (curGame.IsNewGame) {
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
			}
			return metrics;
		}

		/// <summary>
		/// Print all the metrics from this replay.
		/// </summary>
		/// <param name="metrics">A Dictionary containing a list of Metrics by card.</param>
		public void PrintMetrics(Dictionary<String, List<Metric>> metrics) {
			foreach (KeyValuePair<String, List<Metric>> curCardID in metrics) {
				logger.Debug("Entity: " + CardDefs.Cards[curCardID.Key].ShortDescription);
				foreach (Metric curMetric in curCardID.Value) {
					logger.Debug("\t{0} = {1}", curMetric.Name, String.Join(",", curMetric.Values));
				}
			}
		}

		/// <summary>
		/// Parses the HSReplay XML file.
		/// </summary>
		private void Parse() {
			XmlDocument replayDoc = new XmlDocument();
			replayDoc.Load(_filePath);

			XmlNode rootNode = replayDoc.SelectSingleNode("/HSReplay");
			Double.TryParse(rootNode.Attributes["version"].Value, out _version);

			XmlNodeList gameNodeList = replayDoc.SelectNodes("/HSReplay/Game");
			foreach (XmlNode curGame in gameNodeList) {
				_games.Add(new Game(curGame));
				logger.Info("Game Complete.");
				//return;
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
