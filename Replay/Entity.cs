using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	[DebuggerDisplay("{Description}")]
	class Entity {
		public readonly Int32 Id;
		public Dictionary<String, String> Attributes = new Dictionary<String, String>();
		public Dictionary<GameTag, Int32> Tags = new Dictionary<GameTag, Int32>();
		public List<TagChange> TagHistory = new List<TagChange>();

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
				Tags[tagToChange] = newValue;
			} else {
				TagHistory.Add(new TagChange() { IsNew = true, Tag = tagToChange, OldValue = 0, NewValue = newValue, Timestamp = timestamp });
				Tags.Add(tagToChange, newValue);
			}
		}

		public void PrintHistory() {
			Console.WriteLine("Entity History: {0}", Description);
			foreach (TagChange curChange in TagHistory) {
				if (curChange.IsNew) {
					Console.WriteLine("\t{0} = {1}", curChange.Tag, curChange.NewValue);
				} else {
					Console.WriteLine("\t{0} = {1} -> {2}", curChange.Tag, curChange.OldValue, curChange.NewValue);
				}
			}
		}
	}
}
