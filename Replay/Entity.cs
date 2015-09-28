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
	public class Entity {
		public readonly Int32 Id;
		public Dictionary<String, String> Attributes = new Dictionary<String, String>();
		public Dictionary<GameTag, Int32> Tags = new Dictionary<GameTag, Int32>();
		public List<TagChange> TagHistory = new List<TagChange>();
		//public Dictionary<String, Int32> Metrics = new Dictionary<String, Int32>();
		public List<Metric> Metrics = new List<Metric>();

		public String Description {
			get {
				String returnStr = "ID " + Id;
				if (Attributes.ContainsKey("cardID")) {
					returnStr += " - " + Attributes["cardID"];
				}
				return returnStr;
			}
		}

		public Entity(XmlNode entityNode, Game game) {
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
					ChangeOrAddTag(game, tagType, tagValue);
				}
			}
		}

		public void ChangeOrAddTag(Game game, GameTag tagToChange, Int32 newValue, String timestamp = "") {
			if (Tags.ContainsKey(tagToChange)) {
				TagHistory.Add(new TagChange() { IsNew = false, Tag = tagToChange, OldValue = Tags[tagToChange], NewValue = newValue, Timestamp = timestamp });
				if (game.GameEntityObj != null) {
					Metrics = Helpers.IntegrateMetrics(MetricParser.ExtractMetrics(tagToChange, Tags[tagToChange], newValue, false, this, game), Metrics);

				}
				Tags[tagToChange] = newValue;
			} else {
				TagHistory.Add(new TagChange() { IsNew = true, Tag = tagToChange, OldValue = 0, NewValue = newValue, Timestamp = timestamp });
				if (game.GameEntityObj != null) {
					Metrics = Helpers.IntegrateMetrics(MetricParser.ExtractMetrics(tagToChange, -1, newValue, true, this, game), Metrics);
				}
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

		public void PrintMetrics() {
			Console.WriteLine("Entity Metrics: {0}", Description);
			foreach (Metric curMetric in Metrics) {
				Console.WriteLine("\t{0} = {1}", curMetric.Name, String.Join(",", curMetric.Values));
			}
		}


	}
}
