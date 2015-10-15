// <copyright file="Action.cs" company="SpectralCoding.com">
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
	internal class ActionNode : LogNode {
		private Game _game;
		public List<LogNode> Children = new List<LogNode>();
		public Int32 Entity;
		public Int32 Index;
		public Int32 Target;
		public Int32 Type;
		public Double Ts;

		public ActionNode(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// index NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// type NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			Int32.TryParse(xmlNode.Attributes?["index"]?.Value, out Index);
			Int32.TryParse(xmlNode.Attributes?["target"]?.Value, out Target);
			Int32.TryParse(xmlNode.Attributes?["typr"]?.Value, out Type);
			Double.TryParse(xmlNode.Attributes?["ts"]?.Value, out Ts);
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeImporter.Import(childNode, game));
			}
		}

		public override void Process() {
			foreach (LogNode curLogNode in Children) {
				if (curLogNode.GetType() == typeof(ActionNode)) {
					((ActionNode)curLogNode).Process();
				} else if (curLogNode.GetType() == typeof(FullEntityNode)) {
					((FullEntityNode)curLogNode).Process();
				} else if (curLogNode.GetType() == typeof(TagChangeNode)) {
					((TagChangeNode)curLogNode).Process();
				} else if (curLogNode.GetType() == typeof(MetaDataNode)) {
					((MetaDataNode)curLogNode).Process();
				} else if (curLogNode.GetType() == typeof(ShowEntityNode)) {
					((ShowEntityNode)curLogNode).Process();
				} else if (curLogNode.GetType() == typeof(HideEntityNode)) {
					((HideEntityNode)curLogNode).Process();
				} else {
					throw new NotSupportedException();
				}
			}
		}
	}
}