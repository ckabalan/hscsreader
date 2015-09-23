using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;
using HSCSReader.Support.Extensions;

namespace HSCSReader.Replay {
	[DebuggerDisplay("{Description}")]
	class Entity {
		public readonly Int32 Id;
		public Dictionary<String, String> Attributes = new Dictionary<String, String>();
		public Dictionary<GameTag, Int32> Tags = new Dictionary<GameTag, Int32>();
		public List<TagChange> TagHistory = new List<TagChange>();
		public Dictionary<String, Int32> Metrics = new Dictionary<String, Int32>();

		public String Description {
			get {
				String returnStr = "ID " + Id;
				if (Attributes.ContainsKey("cardID")) {
					returnStr += " - " + Attributes["cardID"];
				}
				return returnStr;
			}
		}

		public Entity(XmlNode entityNode) {
			Id = Convert.ToInt32(entityNode.Attributes?["id"]?.Value);
			if (entityNode.Attributes != null) {
				foreach (XmlAttribute curAttr in entityNode.Attributes) {
					Attributes.Add(curAttr.Name, curAttr.Value);
				}
			}
			foreach (XmlNode curTag in entityNode.ChildNodes) {
				if (curTag.Name == "Tag") {
					GameTag tagType = (GameTag)Enum.Parse(typeof(GameTag), curTag.Attributes?["tag"].Value);
					Int32 tagValue = Convert.ToInt32(curTag.Attributes?["value"].Value);
					ChangeOrAddTag(tagType, tagValue);
				}
			}
		}

		public void ChangeOrAddTag(GameTag tagToChange, Int32 newValue, String timestamp = "") {
			if (Tags.ContainsKey(tagToChange)) {
				TagHistory.Add(new TagChange() { IsNew = false, Tag = tagToChange, OldValue = Tags[tagToChange], NewValue = newValue, Timestamp = timestamp });
				UpdateMetrics(tagToChange, Tags[tagToChange], newValue);
				//Console.WriteLine("\t{0} = {1} -> {2}", tagToChange, Helpers.GameTagValueToString(tagToChange, Tags[tagToChange]), Helpers.GameTagValueToString(tagToChange, newValue));
				Tags[tagToChange] = newValue;
			} else {
				TagHistory.Add(new TagChange() { IsNew = true, Tag = tagToChange, OldValue = 0, NewValue = newValue, Timestamp = timestamp });
				//Console.WriteLine("\t{0} = {1}", tagToChange, Helpers.GameTagValueToString(tagToChange, newValue));
                Tags.Add(tagToChange, newValue);
			}
		}

		public void PrintHistory() {
			Console.WriteLine("Entity History: {0}", Description);
			foreach (TagChange curChange in TagHistory) {
				if (curChange.IsNew) {
					Console.WriteLine("\t{0} = {1}", curChange.Tag, Helpers.GameTagValueToString(curChange.Tag, curChange.NewValue));
				} else {
					Console.WriteLine("\t{0} = {1} -> {2}", curChange.Tag, Helpers.GameTagValueToString(curChange.Tag, curChange.OldValue), Helpers.GameTagValueToString(curChange.Tag, curChange.NewValue));
				}
			}
		}

		private void UpdateMetrics(GameTag tagToChange, Int32 oldValue, Int32 newValue) {
			if (MetricTable.Table.ContainsKey(tagToChange)) {
				dynamic oldTypedValue;
				dynamic newTypedValue;
				switch (tagToChange) {
					case GameTag.ZONE:
						oldTypedValue = (Zone)oldValue;
						newTypedValue = (Zone)newValue;
						break;
					default:
						return;
				}
				if (MetricTable.Table[tagToChange].ContainsKey(oldTypedValue)) {
					if (MetricTable.Table[tagToChange][oldTypedValue].ContainsKey(newTypedValue)) {
						String metricName = MetricTable.Table[tagToChange][oldTypedValue][newTypedValue];
						Metrics[metricName] = Metrics.GetValueOrDefault(metricName) + 1;
					} else {
						throw new NotImplementedException(String.Format("Update Metrics: {0}: {1} -> {2}", tagToChange, oldTypedValue, newTypedValue));
					}
				} else {
					throw new NotImplementedException(String.Format("Update Metrics: {0}: {1} -> {2}", tagToChange, oldTypedValue, newTypedValue));
				}
			}
			//	switch (tagToChange) {
			//		case GameTag.ZONE:
			//			if (oldValue == (Int32)Zone.DECK) {
			//				switch ((Zone)newValue) {
			//					case Zone.DISCARD:
			//						Metrics[EntityMetric.DrawnCount] = Metrics.GetValueOrDefault(EntityMetric.DiscardedCount) + 1;
			//						break;
			//					case Zone.HAND:
			//						Metrics[EntityMetric.DrawnCount] = Metrics.GetValueOrDefault(EntityMetric.DrawnCount) + 1;
			//						break;
			//					case Zone.PLAY:
			//						Metrics[EntityMetric.DrawnCount] = Metrics.GetValueOrDefault(EntityMetric.DirectlyToPlayCount) + 1;
			//						break;
			//					case Zone.SECRET:
			//						Metrics[EntityMetric.DrawnCount] = Metrics.GetValueOrDefault(EntityMetric.DirectlyToPlayCount) + 1;
			//						break;
			//					default:
			//						throw new NotImplementedException(String.Format("Update Metrics: Zone.{0} -> Zone.{1}", (Zone)oldValue, (Zone)newValue));
			//				}
			//			}
			//			break;
			//	}
		}
    }
}
