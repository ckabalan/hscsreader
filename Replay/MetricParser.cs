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
					returnList.Add(new Metric($"LIST.ATK", MetricType.AddToList, newValue));
					break;
				case GameTag.ZONE_POSITION:
					returnList.Add(new Metric($"LIST.ZONE_POSITION", MetricType.AddToList, newValue));
					break;
				case GameTag.ZONE:
					if ((Enum.IsDefined(typeof(Zone), oldValue)) && (Enum.IsDefined(typeof(Zone), newValue))) {
						//returnList.Add(new Metric($"LIST.TURNS.ZONE.TO_" + ((Zone)newValue).ToString(), MetricType.AddToList,game.GameEntityObj.Tags[GameTag.TURN]));
						//returnList.Add(new Metric($"LIST.TURNS.ZONE.FROM_" + ((Zone)oldValue).ToString(), MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
						returnList.Add(new Metric($"LIST.TURNS.ZONE." + ((Zone)oldValue).ToString() + "_TO_" + ((Zone)newValue).ToString(), MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
					} else {
						//if (Enum.IsDefined(typeof(Zone), newValue)) {
						//	returnList.Add(new Metric($"LIST.TURNS.ZONE.TO_" + ((Zone)newValue).ToString(), MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
						//}
						//if (Enum.IsDefined(typeof(Zone), oldValue)) {
						//	returnList.Add(new Metric($"LIST.TURNS.ZONE.FROM_" + ((Zone)oldValue).ToString(), MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
						//}
					}
					//if (newValue == (Int32)Zone.PLAY) {
					//	returnList.Add(new Metric($"LIST.TURNS.ZONE.TO_PLAY", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
					//} else if (newValue == (Int32)Zone.GRAVEYARD) {
					//	returnList.Add(new Metric($"LIST.TURNS.ZONE.TO_GRAVEYARD", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
					//} else if (newValue == (Int32)Zone.HAND) {
					//	returnList.Add(new Metric($"LIST.TURNS.ZONE.TO_HAND", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
					//} else if (newValue == (Int32)Zone.SECRET) {
					//	// Should this fall under LIST.TURNS.ZONE.TO_PLAY, or combine on the web side?
					//	returnList.Add(new Metric($"LIST.TURNS.ZONE.TO_SECRET", MetricType.AddToList, game.GameEntityObj.Tags[GameTag.TURN]));
					//}
					break;
				case GameTag.DAMAGE:
					returnList.Add(new Metric($"LIST.DAMAGE", MetricType.AddToList, newValue));
					break;
					//case GameTag.ZONE:
					//	if (isInitialValue) {
					//		metricName = $"SET.{tagToChange}.{(Zone)newValue}";
					//	} else {
					//		metricName = $"CHANGE.{tagToChange}.{(Zone)oldValue}.{(Zone)newValue}";
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.ATTACKING:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.ATTACKING.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.ATTACKING.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.DEFENDING:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.DEFENDING.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.DEFENDING.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.SILENCED:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.SILENCED.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.SILENCED.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.WINDFURY:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.WINDFURY.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.WINDFURY.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.DIVINE_SHIELD:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.DIVINE_SHIELD.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.DIVINE_SHIELD.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.CHARGE:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.CHARGE.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.CHARGE.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.FREEZE:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.FREEZE.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.FREEZE.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.ENRAGED:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.ENRAGED.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.ENRAGED.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.DEATHRATTLE:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.DEATHRATTLE.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.DEATHRATTLE.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.BATTLECRY:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.BATTLECRY.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.BATTLECRY.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
					//case GameTag.FROZEN:
					//	if (newValue == 1) {
					//		metricName = $"COUNT.FROZEN.START";
					//	} else if (newValue == 0) {
					//		metricName = $"COUNT.FROZEN.END";
					//	} else {
					//		throw new NotImplementedException($"Update Metrics: {tagToChange}: {oldValue} -> {newValue}");
					//	}
					//	Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					//	break;
			}
			return returnList;
		}

	}
}
