// <copyright file="ActionNode.cs" company="SpectralCoding.com">
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
using System.Linq;
using System.Xml;
using HSCSReader.Support;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay.LogNodes {
	internal class ActionNode : LogNode {
		private readonly Game _game;
		public readonly List<LogNode> Children = new List<LogNode>();
		public readonly Int32 Entity;
		public readonly Int32 Index;
		public readonly Int32 Target;
		public readonly Double Ts;
		public readonly PowSubType Type;

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
			if (xmlNode.Attributes?["type"]?.Value == null) { throw new NullReferenceException(); }
			Type = (PowSubType)Enum.Parse(typeof(ChoiceType), xmlNode.Attributes?["type"]?.Value);
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
					FullEntityNode tempFullEntityNode = (FullEntityNode)curLogNode;
					if (Type == PowSubType.TRIGGER) {
						// Determine the new entity's starting zone
						Zone newEntityZone = (from tempSubLogNode in tempFullEntityNode.Children
											where tempSubLogNode.GetType() == typeof(TagNode)
											where ((TagNode)tempSubLogNode).Name == GameTag.ZONE
											select (Zone)((TagNode)tempSubLogNode).Value).FirstOrDefault();
						// Determine what to do based on the zone
						switch (newEntityZone) {
							case Zone.HAND:
								// Create Card to Hand
								Helpers.IntegrateMetrics(
														 new List<Metric> {new Metric("COUNTGAME_CARDS_CREATED", MetricType.AddToValue, 1)},
														_game.ActorStates[Entity].Metrics);
								break;
							case Zone.PLAY:
								// Summon Creature
								Helpers.IntegrateMetrics(
														 new List<Metric> {new Metric("COUNTGAME_MINIONS_SUMMONED", MetricType.AddToValue, 1)},
														_game.ActorStates[Entity].Metrics);
								break;
							case Zone.SETASIDE:
								// ToDo: Not Implemented.
								break;
							default:
								throw new NotImplementedException();
						}
					}
					tempFullEntityNode.Process();
				} else if (curLogNode.GetType() == typeof(TagChangeNode)) {
					TagChangeNode tempTagChangeNode = (TagChangeNode)curLogNode;
					if (Type == PowSubType.TRIGGER) {
						if (tempTagChangeNode.Tag == GameTag.ARMOR) {
							Int32 oldValue = 0;
							if (_game.ActorStates[tempTagChangeNode.Entity].Tags.ContainsKey(GameTag.ARMOR)) {
								oldValue = _game.ActorStates[tempTagChangeNode.Entity].Tags[GameTag.ARMOR];
							}
							if (tempTagChangeNode.Value > oldValue) {
								// Prevents certain attacks from showing up as a negative armor gain
								// Example: Boom Bot
								Helpers.IntegrateMetrics(
														 new List<Metric> {
																			new Metric("COUNTGAME_ARMOR_GAIN", MetricType.AddToValue, (tempTagChangeNode.Value - oldValue))
																		},
														_game.ActorStates[Entity].Metrics);
							}
						}
					}
					tempTagChangeNode.Process();
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
