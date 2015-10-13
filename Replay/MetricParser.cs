// /// <copyright file="MetricParser.cs" company="SpectralCoding.com">
// ///     Copyright (c) 2015 SpectralCoding
// /// </copyright>
// /// <license>
// /// This file is part of HSCSReader.
// ///
// /// HSCSReader is free software: you can redistribute it and/or modify
// /// it under the terms of the GNU General Public License as published by
// /// the Free Software Foundation, either version 3 of the License, or
// /// (at your option) any later version.
// ///
// /// HSCSReader is distributed in the hope that it will be useful,
// /// but WITHOUT ANY WARRANTY; without even the implied warranty of
// /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// /// GNU General Public License for more details.
// ///
// /// You should have received a copy of the GNU General Public License
// /// along with HSCSReader.  If not, see <http://www.gnu.org/licenses/>.
// /// </license>
// /// <author>Caesar Kabalan</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay {
	public static class MetricParser {
		public static List<Metric> CalculateStartTurnMetrics(Entity entity, Game game) {
			List<Metric> returnList = new List<Metric>();
			//switch (entity.Tags[GameTag.ZONE]) {
			//	case (Int32)Zone.DECK:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_DECK", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.DISCARD:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_DISCARD", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.GRAVEYARD:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_GRAVEYARD", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.HAND:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_HAND", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.INVALID:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_INVALID", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.PLAY:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_PLAY", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.REMOVEDFROMGAME:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_REMOVEDFROMGAME", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.SECRET:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_SECRET", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.SETASIDE:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_SETASIDE", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//}
			return returnList;
		}

		public static List<Metric> CalculateEndTurnMetrics(Entity entity, Game game) {
			List<Metric> returnList = new List<Metric>();
			//switch (entity.Tags[GameTag.ZONE]) {
			//	case (Int32)Zone.DECK:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_DECK", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.DISCARD:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_DISCARD", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.GRAVEYARD:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_GRAVEYARD", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.HAND:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_HAND", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.INVALID:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_INVALID", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.PLAY:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_PLAY", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.REMOVEDFROMGAME:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_REMOVEDFROMGAME", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.SECRET:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_SECRET", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//	case (Int32)Zone.SETASIDE:
			//		returnList.Add(new Metric($"LIST.TURNS.ZONE.IN_SETASIDE", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
			//		break;
			//}
			return returnList;
		}

		/// <summary>
		/// Extracts a list of metrics that should be changed due to this tag being changed.
		/// </summary>
		/// <param name="tagToChange">The tag which was changed.</param>
		/// <param name="oldValue">The previous value of the tag.</param>
		/// <param name="newValue">The new value of the tag.</param>
		/// <param name="isInitialValue">Whether or not this is the first value for the tag.</param>
		/// <param name="entity">The entity object this tag is modifying.</param>
		/// <param name="game">The game object related to the tag.</param>
		/// <returns>A list of metrics to be changed.</returns>
		public static List<Metric> ExtractTagChangeMetrics(GameTag tagToChange, int oldValue, int newValue,
															bool isInitialValue, Entity entity, Game game) {
			List<Metric> returnList = new List<Metric>();
			//returnList.Add(new Metric($"{tagToChange}: {oldValue} > {newValue}", MetricType.AddToValue, 1));
			switch (tagToChange) {
				case GameTag.NUM_TURNS_IN_PLAY:
					returnList.Add(new Metric($"VALUE.LATEST.NUM_TURNS_IN_PLAY", MetricType.Overwrite, newValue));
					returnList.Add(new Metric($"MAX_NUM_TURNS_IN_PLAY", MetricType.OverwriteMax, newValue));
					break;
				case GameTag.ATK:
					returnList.Add(new Metric($"COUNT_ATK." + newValue, MetricType.AddToValue, 1));
					returnList.Add(new Metric($"MAX_ATK", MetricType.OverwriteMax, newValue));
					break;
				case GameTag.ZONE_POSITION:
					returnList.Add(new Metric($"COUNT_ZONE_POSITION." + newValue, MetricType.AddToValue, 1));
					break;
				case GameTag.ZONE:
					if ((Enum.IsDefined(typeof(Zone), oldValue)) && (Enum.IsDefined(typeof(Zone), newValue))) {
						returnList.Add(
									 new Metric(
										$"COUNT_ZONE_" + ((Zone)oldValue) + "_TO_" + ((Zone)newValue) + "." + game.GameEntityObj.Tags[GameTag.TURN],
										MetricType.AddToValue, 1));
					} else if (oldValue == -1) {
						returnList.Add(new Metric($"COUNT_SEEN", MetricType.AddToValue, 1));
					}
					break;
				case GameTag.DAMAGE:
					returnList.Add(new Metric($"COUNT_DAMAGE." + newValue, MetricType.AddToValue, 1));
					returnList.Add(new Metric($"MAX_DAMAGE", MetricType.OverwriteMax, newValue));
					break;
				case GameTag.ARMOR:
					break;
			}
			return returnList;
		}

		public static List<Metric> ExtractActionMetrics() { }
	}
}