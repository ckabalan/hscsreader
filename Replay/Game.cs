using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.DataStorage;
using HSCSReader.Support;
using HSCSReader.Support.HSEnumerations;
using NLog;

namespace HSCSReader.Replay {
	public class Game {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private String _ts;
		public Dictionary<Int32, Entity> Entities = new Dictionary<Int32, Entity>();
		public GameEntity GameEntityObj;
		public String Md5Hash = String.Empty;
		public Boolean IsNewGame = false;

		public Game(XmlNode gameNode) {
            Md5Hash = Helpers.GetMd5Hash(MD5.Create(), gameNode.OuterXml);
			logger.Trace($"Calculated MD5 from Game XML: {Md5Hash}");
			IsNewGame = Uploader.IsNewGame(Md5Hash);
			IsNewGame = true;
            if (IsNewGame) {
				logger.Info($"MD5 Hash did not exist, parsing game...");
				_ts = gameNode.Attributes?["ts"]?.Value;
				foreach (XmlNode childNode in gameNode.ChildNodes) {
					ProcessNode(childNode);
				}
				//GameEntityObj.PrintHistory();
				foreach (KeyValuePair<Int32, Entity> curKVP in Entities) {
					curKVP.Value.PrintMetrics();
				}
			} else {
				logger.Info($"MD5 Hash exists, skipping game...");
			}
		}

		private void ProcessNode(XmlNode xmlNode) {
			switch (xmlNode.Name) {
				case "Action": Action(xmlNode); break;
				case "Choices": Choices(xmlNode); break;
				case "FullEntity": FullEntity(xmlNode); break;
				case "GameEntity": GameEntity(xmlNode); break;
				case "HideEntity": HideEntity(xmlNode); break;
				case "Info": Info(xmlNode); break;
				case "MetaData": MetaData(xmlNode); break;
				case "Options": Options(xmlNode); break;
				case "Player": Player(xmlNode); break;
				case "SendChoices": SendChoices(xmlNode); break;
				case "SendOption": SendOption(xmlNode); break;
				case "ShowEntity": ShowEntity(xmlNode); break;
				case "TagChange": TagChange(xmlNode); break;
				case "Target": Target(xmlNode); break;
				default: throw new NotImplementedException();
			}
		}

		private void Action(XmlNode xmlNode) {
			// entity % entity; #REQUIRED
			// index NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			//Console.WriteLine("Action Start.");
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				ProcessNode(childNode);
			}
			//Console.WriteLine("Action End.");
		}

		private void Choices(XmlNode xmlNode) {
			// entity % entity; #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// source NMTOKEN #REQUIRED
			// type NMTOKEN #REQUIRED
			// min NMTOKEN #IMPLIED
			// max NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
		}

		private void FullEntity(XmlNode xmlNode) {
			// cardID NMTOKEN #IMPLIED
			// id % gameTag; #REQUIRED
			// ts NMTOKEN #IMPLIED
			Int32 newId = Convert.ToInt32(xmlNode.Attributes?["id"].Value);
			Entities.Add(newId, new Entity(xmlNode, this));
        }

		private void GameEntity(XmlNode xmlNode) {
			// id %entity; #REQUIRED
			Int32 newId = Convert.ToInt32(xmlNode.Attributes?["id"].Value);
			GameEntityObj = new GameEntity(xmlNode, this);
            Entities.Add(newId, GameEntityObj);
		}

		private void HideEntity(XmlNode xmlNode) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
		}

		private void MetaData(XmlNode xmlNode) {
			// meta NMTOKEN #REQUIRED
			// data % entity; #IMPLIED
			// info NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
		}

		private void Info(XmlNode xmlNode) {
			// index NMTOKEN #REQUIRED
			// id % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
		}

		private void Options(XmlNode xmlNode) {
			// id NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
		}

		private void Player(XmlNode xmlNode) {
			// id NMTOKEN #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// name CDATA #IMPLIED
			// accountHi NMTOKEN #IMPLIED
			// accountLo NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			Int32 newId = Convert.ToInt32(xmlNode.Attributes?["id"].Value);
			Entities.Add(newId, new Entity(xmlNode, this));
		}
		private void SendChoices(XmlNode xmlNode) {
			// entity % entity; #REQUIRED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
		}
		private void SendOption(XmlNode xmlNode) {
			// option NMTOKEN #REQUIRED
			// subOption NMTOKEN #IMPLIED
			// position NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
		}
		private void ShowEntity(XmlNode xmlNode) {
			// cardID NMTOKEN #IMPLIED
			// entity % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
		}

		private void TagChange(XmlNode xmlNode) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			if (xmlNode.Attributes?["entity"].Value != "[UNKNOWN HUMAN PLAYER]") {
				Int32 entityId = Convert.ToInt32(xmlNode.Attributes?["entity"].Value);
				GameTag entityTag = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"].Value);
				Int32 newValue = Convert.ToInt32(xmlNode.Attributes?["value"].Value);
				Entities[entityId].ChangeOrAddTag(this, entityTag, newValue);
			}
		}

		private void Target(XmlNode xmlNode) {
			// entity %entity; #REQUIRED
			// index NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
		}
	}
}
