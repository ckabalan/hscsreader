// <copyright file="NodeImporter.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.Replay.LogNodes;

namespace HSCSReader.Replay {
	internal static class NodeImporter {
		public static LogNode Import(XmlNode xmlNode, Game game) {
			switch (xmlNode.Name) {
				case "Action":
					return Action(xmlNode, game);
				case "Choice":
					return Choice(xmlNode, game);
				case "Choices":
					return Choices(xmlNode, game);
				case "ChosenEntities":
					return ChosenEntities(xmlNode, game);
				case "FullEntity":
					return FullEntity(xmlNode, game);
				case "GameEntity":
					return GameEntity(xmlNode, game);
				case "HideEntity":
					return HideEntity(xmlNode, game);
				case "Info":
					return Info(xmlNode, game);
				case "MetaData":
					return MetaData(xmlNode, game);
				case "Option":
					return Option(xmlNode, game);
				case "Options":
					return Options(xmlNode, game);
				case "Player":
					return Player(xmlNode, game);
				case "SendChoices":
					return SendChoices(xmlNode, game);
				case "SendOption":
					return SendOption(xmlNode, game);
				case "ShowEntity":
					return ShowEntity(xmlNode, game);
				case "TagChange":
					return TagChange(xmlNode, game);
				case "Tag":
					return Tag(xmlNode, game);
				case "Target":
					return Target(xmlNode, game);
				default:
					throw new NotImplementedException();
			}
		}

		private static ActionNode Action(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// index NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new ActionNode(xmlNode, game);
		}

		private static ChoiceNode Choice(XmlNode xmlNode, Game game) {
			// entity %entity; #REQUIRED
			// index NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new ChoiceNode(xmlNode, game);
		}

		private static ChoicesNode Choices(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// source NMTOKEN #REQUIRED
			// type NMTOKEN #REQUIRED
			// min NMTOKEN #IMPLIED
			// max NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			return new ChoicesNode(xmlNode, game);
		}

		private static ChosenEntitiesNode ChosenEntities(XmlNode xmlNode, Game game) {
			// ToDo: Not Fully Implemented. Still waiting on official DTD to finish.
			return new ChosenEntitiesNode(xmlNode, game);
		}

		private static FullEntityNode FullEntity(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// id % gameTag; #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new FullEntityNode(xmlNode, game);
		}

		private static GameEntityNode GameEntity(XmlNode xmlNode, Game game) {
			// id %entity; #REQUIRED
			return new GameEntityNode(xmlNode, game);
		}

		private static HideEntityNode HideEntity(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new HideEntityNode(xmlNode, game);
		}

		private static MetaDataNode MetaData(XmlNode xmlNode, Game game) {
			// meta NMTOKEN #REQUIRED
			// data % entity; #IMPLIED
			// info NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new MetaDataNode(xmlNode, game);
		}

		private static InfoNode Info(XmlNode xmlNode, Game game) {
			// index NMTOKEN #REQUIRED
			// id % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new InfoNode(xmlNode, game);
		}

		private static OptionNode Option(XmlNode xmlNode, Game game) {
			// entity % entity; #IMPLIED
			// index NMTOKEN #REQUIRED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new OptionNode(xmlNode, game);
		}

		private static OptionsNode Options(XmlNode xmlNode, Game game) {
			// id NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new OptionsNode(xmlNode, game);
		}

		private static PlayerNode Player(XmlNode xmlNode, Game game) {
			// id NMTOKEN #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// name CDATA #IMPLIED
			// accountHi NMTOKEN #IMPLIED
			// accountLo NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			return new PlayerNode(xmlNode, game);
		}

		private static SendChoicesNode SendChoices(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new SendChoicesNode(xmlNode, game);
		}

		private static SendOptionNode SendOption(XmlNode xmlNode, Game game) {
			// option NMTOKEN #REQUIRED
			// subOption NMTOKEN #IMPLIED
			// position NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			return new SendOptionNode(xmlNode, game);
		}

		private static ShowEntityNode ShowEntity(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// entity % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new ShowEntityNode(xmlNode, game);
		}

		private static TagNode Tag(XmlNode xmlNode, Game game) {
			// 	tag %gameTag; #REQUIRED
			//	value NMTOKEN #REQUIRED
			//	ts NMTOKEN #IMPLIED
			return new TagNode(xmlNode, game);
		}

		private static TagChangeNode TagChange(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			//if (xmlNode.Attributes?["entity"].Value != "[UNKNOWN HUMAN PLAYER]") {
			//	Int32 entityId = Convert.ToInt32(xmlNode.Attributes?["entity"].Value);
			//	GameTag entityTag = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"].Value);
			//	Int32 newValue = Convert.ToInt32(xmlNode.Attributes?["value"].Value);
			//	Entities[entityId].ChangeOrAddTag(this, entityTag, newValue);
			//}
			return new TagChangeNode(xmlNode, game);
		}

		private static TargetNode Target(XmlNode xmlNode, Game game) {
			// entity %entity; #REQUIRED
			// index NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			return new TargetNode(xmlNode, game);
		}
	}
}