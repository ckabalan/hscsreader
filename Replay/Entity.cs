using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.Enumerations;

namespace HSCSReader.Replay {
	class Entity {
		public readonly Int32 Id;
		public Dictionary<String, String> Attributes = new Dictionary<String, String>();
		public Dictionary<GameTag, Int32> Tags = new Dictionary<GameTag, Int32>();

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
                    Tags.Add(tagType, tagValue);
				}
			}
		}

		public void ChangeOrAddTag(GameTag tagToChange, Int32 newValue) {
			if (Tags.ContainsKey(tagToChange)) {
				Tags[tagToChange] = newValue;
			}
			else {
				Tags.Add(tagToChange, newValue);
			}
		}

	}
}
