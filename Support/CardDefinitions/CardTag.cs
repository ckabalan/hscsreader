// <copyright file="CardTag.cs" company="SpectralCoding.com">
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
using System.Linq;
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support.CardDefinitions {
	public class CardTag {
		public readonly dynamic Value;

		/// <summary>
		/// Initializes a new instance of the CardTag class.
		/// </summary>
		/// <param name="xmlNode">The XML Node which describes the card's tag.</param>
		public CardTag(XmlNode xmlNode) {
			switch (xmlNode.Attributes?["type"].Value) {
				case "LocString":
					Dictionary<String, String> tempLocDict =
						xmlNode.ChildNodes.Cast<XmlNode>()
								.ToDictionary(curLangNode => curLangNode.Name, curLangNode => curLangNode.InnerText);
					//Dictionary<String, String> tempLocDict = new Dictionary<String, String>();
					//foreach (XmlNode curLangNode in xmlNode.ChildNodes) {
					//	tempLocDict.Add(curLangNode.Name, curLangNode.InnerText);
					//}
					Value = tempLocDict;
					break;
				case "CardSet":
					Value = (CardSet)(Convert.ToInt32(xmlNode.Attributes?["value"].Value));
					break;
				case "Race":
					Value = (Race)(Convert.ToInt32(xmlNode.Attributes?["value"].Value));
					break;
				case "Faction":
					Value = (Faction)(Convert.ToInt32(xmlNode.Attributes?["value"].Value));
					break;
				case "Bool":
					if (xmlNode.Attributes?["value"].Value == "1") {
						Value = true;
					} else if (xmlNode.Attributes?["value"].Value == "0") {
						Value = false;
					} else {
						throw new NotSupportedException("Bool Value Not Supported: " + xmlNode.Attributes["value"].Value);
					}
					break;
				case "Rarity":
					Value = (Rarity)Convert.ToInt32(xmlNode.Attributes?["value"].Value);
					break;
				case "CardType":
					Value = (CardType)(Convert.ToInt32(xmlNode.Attributes?["value"].Value));
					break;
				case "Class":
					Value = (CardClass)(Convert.ToInt32(xmlNode.Attributes?["value"].Value));
					break;
				case "EnchantmentVisualType":
					Value = (EnchantmentVisual)(Convert.ToInt32(xmlNode.Attributes?["value"].Value));
					break;
				case "String":
					Value = xmlNode.Attributes?["value"] != null ? xmlNode.Attributes["value"].Value : xmlNode.InnerText;
					break;
				case "Int":
				case "AttackVisualType": // Unused
				case "DevState": // Unused
				case "":
					// <Dandelock> jleclanche, FYI in CardDefs.xml you have a few Tag's without a data type. Example: <Tag enumID="380" type="" value="2381"/>
					// <jleclanche> Dandelock: type is not a guarantee
					// <jleclanche> it's int if it's empty
					// <Dandelock> jleclanche, thanks.
					Value = Convert.ToInt32(xmlNode.Attributes?["value"].Value);
					break;
				default:
					throw new NotImplementedException("Unknown Tag Type: " + xmlNode.Attributes?["type"].Value);
			}
		}
	}
}