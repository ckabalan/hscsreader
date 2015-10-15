// <copyright file="GameEntity.cs" company="SpectralCoding.com">
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
	public class GameEntityNode : LogNode {
		private Game _game;
		public List<LogNode> Children = new List<LogNode>();
		public Int32 Id;

		public GameEntityNode(XmlNode xmlNode, Game game) {
			// id %entity; #REQUIRED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["id"]?.Value, out Id);
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		public override void Process() {
			GameEntityState tempState = new GameEntityState();
			tempState.Id = Id;
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