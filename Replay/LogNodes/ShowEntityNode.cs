// <copyright file="ShowEntity.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.Replay.EntityStates;

namespace HSCSReader.Replay.LogNodes {
	internal class ShowEntityNode : LogNode {
		private Game _game;
		public List<LogNode> Children = new List<LogNode>();
		public String CardId;
		public Int32 Entity;
		public String Ts;

		public ShowEntityNode(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// entity % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			CardId = xmlNode.Attributes?["cardID"]?.Value;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			Ts = xmlNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		public override void Process() {
			//ShowEntityState tempState = new ShowEntityState();
			//tempState.CardId = CardId;
			//tempState.Ts = Ts;
			//_game.ActorStates.Add(Id, tempState);
			//foreach (LogNode curLogNode in Children) {
			//	if (curLogNode.GetType() == typeof(TagNode)) {
			//		((TagNode)curLogNode).Process(Id);
			//	} else {
			//		throw new NotSupportedException();
			//	}
			//}
		}
	}
}