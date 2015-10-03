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

		public static List<Metric> ExtractTagChangeMetrics(GameTag tagToChange, Int32 oldValue, Int32 newValue, Boolean isInitialValue, Entity entity, Game game) {
			List<Metric> returnList = new List<Metric>();
			//returnList.Add(new Metric($"{tagToChange}: {oldValue} > {newValue}", MetricType.AddToValue, 1));
			switch (tagToChange) {
				case GameTag.NUM_TURNS_IN_PLAY:
					returnList.Add(new Metric($"VALUE.LATEST.NUM_TURNS_IN_PLAY", MetricType.Overwrite, newValue));
					break;
				case GameTag.ATK:
					returnList.Add(new Metric($"COUNT.ATK." + newValue, MetricType.AddToValue, 1));
					break;
				case GameTag.ZONE_POSITION:
					returnList.Add(new Metric($"COUNT.ZONE_POSITION." + newValue, MetricType.AddToValue, 1));
					break;
				case GameTag.ZONE:
					if ((Enum.IsDefined(typeof(Zone), oldValue)) && (Enum.IsDefined(typeof(Zone), newValue))) {
						returnList.Add(new Metric($"COUNT.TURNS.ZONE." + ((Zone)oldValue).ToString() + "_TO_" + ((Zone)newValue).ToString() + "." + game.GameEntityObj.Tags[GameTag.TURN], MetricType.AddToValue, 1));
					} else if (oldValue == -1) {
						returnList.Add(new Metric($"COUNT.SEEN", MetricType.AddToValue, 1));
					}
					break;
				case GameTag.DAMAGE:
					returnList.Add(new Metric($"COUNT.DAMAGE." + newValue, MetricType.AddToValue, 1));
					break;
			}
			return returnList;
		}

	}
}
