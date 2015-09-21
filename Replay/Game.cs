using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	class Game {
		private String _ts;
		private Dictionary<Int32, Entity> _entities = new Dictionary<Int32, Entity>();

		public Game(XmlNode gameNode) {
			_ts = gameNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in gameNode.ChildNodes) {
				ProcessNode(childNode);
			}
		}

		private void ProcessNode(XmlNode xmlNode) {
			switch (xmlNode.Name) {
				case "Action":
					break;
				case "Choices":
					break;
				case "FullEntity":
					AddEntity(xmlNode);
					break;
				case "GameEntity":
					AddEntity(xmlNode);
					break;
				case "HideEntity":
					break;
				case "Options":
					// This one looks complex.
					break;
				case "Player":
					AddEntity(xmlNode);
					break;
				case "SendChoices":
					break;
				case "SendOption":
					break;
				case "ShowEntity":
					break;
				case "TagChange":
					ChangeTag(xmlNode);
					break;
			}
		}

		private void AddEntity(XmlNode xmlNode) {
			// ID = Required
			Int32 newId = Convert.ToInt32(xmlNode.Attributes?["id"].Value);
            _entities.Add(newId, new Entity(xmlNode));
		}

		private void ChangeTag(XmlNode xmlNode) {
			// Entity = Required
			// Tag = Required
			// Value = Required
			Int32 entityId = Convert.ToInt32(xmlNode.Attributes?["entity"].Value);
            GameTag entityTag = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"].Value);
			Int32 newValue = Convert.ToInt32(xmlNode.Attributes?["value"].Value);
			_entities[entityId].ChangeOrAddTag(entityTag, newValue);
		}
	}
}
