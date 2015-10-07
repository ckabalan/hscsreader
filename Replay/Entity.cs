using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support;
using HSCSReader.Support.CardDefinitions;
using HSCSReader.Support.Enumerations;
using HSCSReader.Support.HSEnumerations;
using HSCSReader.Support.Extensions;
using NLog;

namespace HSCSReader.Replay {
	[DebuggerDisplay("{Description}")]
	public class Entity {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public readonly Int32 Id;
		public Dictionary<String, String> Attributes = new Dictionary<String, String>();
		public Dictionary<GameTag, Int32> Tags = new Dictionary<GameTag, Int32>();
		public List<TagChange> TagHistory = new List<TagChange>();
		public List<Metric> Metrics = new List<Metric>();

		/// <summary>
		/// The description of this entity.
		/// </summary>
		public String Description {
			get {
				String returnStr = "ID " + Id;
				if (Attributes.ContainsKey("cardID")) {
					returnStr += " - " + CardDefs.Cards[Attributes["cardID"]].ShortDescription;
				}
				return returnStr;
			}
		}

		/// <summary>
		/// Initializes a new instance of the Entity class.
		/// </summary>
		/// <param name="entityNode">The XML Node describing the Entity.</param>
		/// <param name="game">The game object related to the entity.</param>
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

		/// <summary>
		/// Modifies or creates a new tag for the entity.
		/// </summary>
		/// <param name="game">The game object related to the entity.</param>
		/// <param name="tagToChange">The GameTag object to change.</param>
		/// <param name="newValue">The new value of the tag.</param>
		/// <param name="timestamp">The optional timestamp of the tag.</param>
		public virtual void ChangeOrAddTag(Game game, GameTag tagToChange, Int32 newValue, String timestamp = "") {
			if (Tags.ContainsKey(tagToChange)) {
				TagHistory.Add(new TagChange() { IsNew = false, Tag = tagToChange, OldValue = Tags[tagToChange], NewValue = newValue, Timestamp = timestamp });
				if (game.GameEntityObj != null) {
					Metrics = Helpers.IntegrateMetrics(MetricParser.ExtractTagChangeMetrics(tagToChange, Tags[tagToChange], newValue, false, this, game), Metrics);

				}
				Tags[tagToChange] = newValue;
			} else {
				TagHistory.Add(new TagChange() { IsNew = true, Tag = tagToChange, OldValue = 0, NewValue = newValue, Timestamp = timestamp });
				if (game.GameEntityObj != null) {
					Metrics = Helpers.IntegrateMetrics(MetricParser.ExtractTagChangeMetrics(tagToChange, -1, newValue, true, this, game), Metrics);
				}
				Tags.Add(tagToChange, newValue);
			}
		}

		/// <summary>
		/// Calculates metrics that should be re-evaluated at the beginning of a turn.
		/// </summary>
		/// <param name="game">The game object related to the entity.</param>
		/// <param name="timestamp">The optional timestamp of the tag.</param>
		public void StartTurn(Game game, String timestamp = "") {
			Metrics = Helpers.IntegrateMetrics(MetricParser.CalculateStartTurnMetrics(this, game), Metrics);
		}

		/// <summary>
		/// Calculates metrics that should be re-evaluated at the ending of a turn.
		/// </summary>
		/// <param name="game">The game object related to the entity.</param>
		/// <param name="timestamp">The optional timestamp of the tag.</param>
		public void EndTurn(Game game, String timestamp = "") {
			Metrics = Helpers.IntegrateMetrics(MetricParser.CalculateEndTurnMetrics(this, game), Metrics);
		}

		/// <summary>
		/// Prints the tag change history for this entity.
		/// </summary>
		public void PrintHistory() {
			logger.Debug("Entity History: {0}", Description);
			foreach (TagChange curChange in TagHistory) {
				if (curChange.IsNew) {
					logger.Debug("\t{0} = {1}", curChange.Tag, Helpers.GameTagValueToString(curChange.Tag, curChange.NewValue));
				} else {
					logger.Debug("\t{0} = {1} -> {2}", curChange.Tag, Helpers.GameTagValueToString(curChange.Tag, curChange.OldValue), Helpers.GameTagValueToString(curChange.Tag, curChange.NewValue));
				}
			}
		}

		/// <summary>
		/// Prints all the metrics related to this entity.
		/// </summary>
		public void PrintMetrics() {
			logger.Debug("Entity Metrics: {0}", Description);
			foreach (Metric curMetric in Metrics) {
				logger.Debug("\t{0} = {1}", curMetric.Name, String.Join(",", curMetric.Values));
			}
		}


	}
}
