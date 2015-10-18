// <copyright file="PlayerNode.cs" company="SpectralCoding.com">
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
	internal class PlayerNode : LogNode {
		private readonly Game _game;
		public readonly String AccountHi;
		public readonly String AccountLo;
		public readonly List<LogNode> Children = new List<LogNode>();
		public readonly Int32 Id;
		public readonly String Name;
		public readonly Int32 PlayerId;
		public readonly String Ts;

		public PlayerNode(XmlNode xmlNode, Game game) {
			// id NMTOKEN #REQUIRED
			// playerID NMTOKEN #REQUIRED
			// name CDATA #IMPLIED
			// accountHi NMTOKEN #IMPLIED
			// accountLo NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["id"]?.Value, out Id);
			Int32.TryParse(xmlNode.Attributes?["playerID"]?.Value, out PlayerId);
			Name = xmlNode.Attributes?["name"]?.Value;
			AccountHi = xmlNode.Attributes?["accountHi"]?.Value;
			AccountLo = xmlNode.Attributes?["accountLo"]?.Value;
			Ts = xmlNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		public override void Process() {
			PlayerState tempState = new PlayerState {
														Id = Id,
														PlayerId = PlayerId,
														Name = Name,
														AccountHi = AccountHi,
														AccountLo = AccountLo,
														Ts = Ts
													};
			_game.ActorStates.Add(Id, tempState);
			foreach (LogNode curLogNode in Children) {
				if (curLogNode.GetType() == typeof(TagNode)) {
					((TagNode)curLogNode).Process(Id);
				} else {
					throw new NotSupportedException();
				}
			}
		}
	}
}
