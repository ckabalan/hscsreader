using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support.CardDefinitions {
	public class CardTag {
		public dynamic Value;
		public CardTag(XmlNode xmlNode) {
			switch (xmlNode.Attributes["type"].Value) {
				case "LocString":
					Dictionary<String, String> tempLocDict = new Dictionary<String, String>();
					foreach (XmlNode curLangNode in xmlNode.ChildNodes) {
						tempLocDict.Add(curLangNode.Name, curLangNode.InnerText);
					}
					Value = tempLocDict;
					break;
				case "CardSet":
					Value = (CardSet)(Convert.ToInt32(xmlNode.Attributes["value"].Value));
					break;
				case "Race":
					Value = (Race)(Convert.ToInt32(xmlNode.Attributes["value"].Value));
					break;
				case "Faction":
					Value = (Faction)(Convert.ToInt32(xmlNode.Attributes["value"].Value));
					break;
				case "Bool":
					if (xmlNode.Attributes["value"].Value == "1") {
						Value = true;
					} else if (xmlNode.Attributes["value"].Value == "0") {
						Value = false;
					} else {
						throw new NotSupportedException("Bool Value Not Supported: " + xmlNode.Attributes["value"].Value);
					}
					break;
				case "Rarity":
					Value = (Rarity)Convert.ToInt32(xmlNode.Attributes["value"].Value);
					break;
				case "CardType":
					Value = (CardType)(Convert.ToInt32(xmlNode.Attributes["value"].Value));
					break;
				case "Class":
					Value = (CardClass)(Convert.ToInt32(xmlNode.Attributes["value"].Value));
					break;
				case "EnchantmentVisualType":
					Value = (EnchantmentVisual)(Convert.ToInt32(xmlNode.Attributes["value"].Value));
					break;
				case "String":
					if (xmlNode.Attributes["value"] != null) {
						Value = xmlNode.Attributes["value"].Value;
					} else {
						Value = xmlNode.InnerText;
					}
					break;
				case "Int":
				case "AttackVisualType":	// Unused
				case "DevState":			// Unused
				case "":
					// <Dandelock> jleclanche, FYI in CardDefs.xml you have a few Tag's without a data type. Example: <Tag enumID="380" type="" value="2381"/>
					// <jleclanche> Dandelock: type is not a guarantee
					// <jleclanche> it's int if it's empty
					// <Dandelock> jleclanche, thanks.
					Value = Convert.ToInt32(xmlNode.Attributes["value"].Value);
					break;
				default:
					throw new NotImplementedException("Unknown Tag Type: " + xmlNode.Attributes["type"].Value);
			}
		}
	}
}
