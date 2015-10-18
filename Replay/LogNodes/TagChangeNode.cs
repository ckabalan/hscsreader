// <copyright file="TagChangeNode.cs" company="SpectralCoding.com">
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
using HSCSReader.Support;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;
using NLog;
using Logger = NLog.Logger;

namespace HSCSReader.Replay.LogNodes {
	internal class TagChangeNode : LogNode {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly Game _game;
		public readonly Int32 Entity;
		public readonly GameTag Tag;
		public readonly String Ts;
		public readonly Int32 Value;

		public TagChangeNode(XmlNode xmlNode, Game game) {
			// entity % entity; #REQUIRED
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			if (xmlNode.Attributes?["tag"]?.Value == null) { throw new NullReferenceException(); }
			Tag = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"]?.Value);
			Int32.TryParse(xmlNode.Attributes?["value"]?.Value, out Value);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}

		public override void Process() {
			if (Entity == 0) {
				// What?! Invalid Replay?
				// Hopefully we can just ignore this tag change?
				// Maybe we should discard the whole game?
				return;
			}
			if (!_game.ActorStates[Entity].Tags.ContainsKey(Tag)) {
				// I'm not sure if this is correct. It might come back to bite me later.
				// We're saying any unset tag is actually set to -1.
				_game.ActorStates[Entity].Tags.Add(Tag, -1);
			}
			Int32 oldValue = _game.ActorStates[Entity].Tags[Tag];
			_game.ActorStates[Entity].Tags[Tag] = Value;
			List<Metric> newMetrics = new List<Metric>();
			switch (Tag) {
				case GameTag.STEP:
					//Logger.Debug($"Step Changed: {Helpers.GameTagValueToString(GameTag.STEP, oldValue)} -> {Helpers.GameTagValueToString(GameTag.STEP, Value)}");
					break;
				case GameTag.TURN:
					//Logger.Debug($"Turn Changed: {oldValue} -> {Value}");
					foreach (KeyValuePair<Int32, EntityState> curKVP in _game.ActorStates) {
						List<Metric> innerMetrics = new List<Metric>();
						if (curKVP.Value.Tags.ContainsKey(GameTag.ZONE)) {
							Zone entityZone = (Zone)curKVP.Value.Tags[GameTag.ZONE];
							innerMetrics.Add(new Metric("COUNT_IN_ZONE_" + entityZone + "." + oldValue, MetricType.Overwrite, 1));
							innerMetrics.Add(new Metric("COUNT_IN_ZONE_" + entityZone + "." + Value, MetricType.Overwrite, 1));
						}
						Helpers.IntegrateMetrics(innerMetrics, curKVP.Value.Metrics);
					}
					break;
				case GameTag.NUM_TURNS_IN_PLAY:
					newMetrics.Add(new Metric("VALUE.LATEST.NUM_TURNS_IN_PLAY", MetricType.Overwrite, Value));
					newMetrics.Add(new Metric("MAX_NUM_TURNS_IN_PLAY", MetricType.OverwriteMax, Value));
					break;
				case GameTag.ATK:
					newMetrics.Add(new Metric("COUNT_ATK." + Value, MetricType.AddToValue, 1));
					newMetrics.Add(new Metric("MAX_ATK", MetricType.OverwriteMax, Value));
					break;
				case GameTag.ZONE_POSITION:
					newMetrics.Add(new Metric("COUNT_ZONE_POSITION." + Value, MetricType.AddToValue, 1));
					break;
				case GameTag.ZONE:
					if ((Enum.IsDefined(typeof(Zone), oldValue)) && (Enum.IsDefined(typeof(Zone), Value))) {
						newMetrics.Add(
									 new Metric(
										"COUNT_ZONE_" + ((Zone)oldValue) + "_TO_" + ((Zone)Value) + "." + _game.ActorStates[1].Tags[GameTag.TURN],
										MetricType.AddToValue,
										1));
						newMetrics.Add(new Metric("COUNT_IN_ZONE_" + ((Zone)oldValue) + "." + _game.ActorStates[1].Tags[GameTag.TURN],
												MetricType.Overwrite,
												1));
						newMetrics.Add(new Metric("COUNT_IN_ZONE_" + ((Zone)Value) + "." + _game.ActorStates[1].Tags[GameTag.TURN],
												MetricType.Overwrite,
												1));
					} else if (oldValue == -1) {
						newMetrics.Add(new Metric("COUNT_SEEN", MetricType.AddToValue, 1));
					}
					break;
				case GameTag.DAMAGE:
					newMetrics.Add(new Metric("COUNT_DAMAGE." + Value, MetricType.AddToValue, 1));
					newMetrics.Add(new Metric("MAX_DAMAGE", MetricType.OverwriteMax, Value));
					break;
				case GameTag.ARMOR:
					break;
			}
			Helpers.IntegrateMetrics(newMetrics, _game.ActorStates[Entity].Metrics);
		}
	}
}
