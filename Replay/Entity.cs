using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support;
using HSCSReader.Support.CardDefinitions;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;
using HSCSReader.Support.Extensions;
using NLog;

namespace HSCSReader.Replay {
	[DebuggerDisplay("{Description}")]
	public class Entity {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public readonly Int32 Id;
		public Dictionary<String, String> Attributes = new Dictionary<String, String>();
		public Dictionary<GameTag, Int32> Tags = new Dictionary<GameTag, Int32>();

		/// <summary>
		/// The description of this entity.
		/// </summary>
		public String Description {
			get {
				String returnStr = "ID " + Id;
				if (Attributes.ContainsKey("cardID")) {
					returnStr += " - " + CardDefs.Cards[Attributes["cardID"]].ShortDescription;
				}
				return returnStr;
			}
		}

		///// <summary>
		///// Initializes a new instance of the Entity class.
		///// </summary>
		///// <param name="entityNode">The XML Node describing the Entity.</param>
		///// <param name="game">The game object related to the entity.</param>
		//public Entity(XmlNode entityNode, Game game) {
		//	Id = Convert.ToInt32(entityNode.Attributes?["id"]?.Value);
		//	if (entityNode.Attributes != null) {
		//		foreach (XmlAttribute curAttr in entityNode.Attributes) {
		//			Attributes.Add(curAttr.Name, curAttr.Value);
		//		}
		//	}
		//	foreach (XmlNode curTag in entityNode.ChildNodes) {
		//		if (curTag.Name == "Tag") {
		//			GameTag tagType = (GameTag)Enum.Parse(typeof(GameTag), curTag.Attributes?["tag"].Value);
		//			Int32 tagValue = Convert.ToInt32(curTag.Attributes?["value"].Value);
		//			ChangeOrAddTag(game, tagType, tagValue);
		//		}
		//	}
		//}

	}
}
