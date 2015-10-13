// <copyright file="Entity.cs" company="SpectralCoding.com">
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
		public readonly int Id;
		public Dictionary<string, string> Attributes = new Dictionary<string, string>();
		public Dictionary<GameTag, int> Tags = new Dictionary<GameTag, int>();

		/// <summary>
		/// The description of this entity.
		/// </summary>
		public string Description {
			get {
				string returnStr = "ID " + Id;
				if (Attributes.ContainsKey("cardID")) {
					returnStr += " - " + CardDefs.Cards[Attributes["cardID"]].ShortDescription;
				}
				return returnStr;
			}
		}

		///// <param name="game">The game object related to the entity.</param>
		///// <param name="entityNode">The XML Node describing the Entity.</param>
		///// </summary>
		///// Initializes a new instance of the Entity class.

		///// <summary>
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