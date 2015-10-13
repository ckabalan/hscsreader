// /// <copyright file="Card.cs" company="SpectralCoding.com">
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
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.HSEnumerations;
using HSCSReader.Support;
using HSCSReader.Support.Extensions;

namespace HSCSReader.Support.CardDefinitions {
	[DebuggerDisplay("{ShortDescription}")]
	public class Card {
		public string CardID;
		public string MasterPower;
		public Dictionary<string, Dictionary<PlayReq, string>> Powers = new Dictionary<string, Dictionary<PlayReq, string>>();
		public Dictionary<GameTag, CardTag> Tags = new Dictionary<GameTag, CardTag>();
		public int Version;

		/// <summary>
		/// Initializes the instance of the Card class.
		/// </summary>
		/// <param name="xmlNode">The XML Node which described the card.</param>
		public Card(XmlNode xmlNode) {
			CardID = xmlNode.Attributes?["CardID"]?.Value;
			Version = Convert.ToInt32(xmlNode.Attributes?["version"]?.Value);
			foreach (XmlNode curSubNode in xmlNode.ChildNodes) {
				switch (curSubNode.Name) {
					case "Tag":
						int tempTagID = Convert.ToInt32(curSubNode.Attributes["enumID"].Value);
						if (Tags.ContainsKey((GameTag)tempTagID)) {
							Tags[(GameTag)tempTagID] = new CardTag(curSubNode);
						} else {
							Tags.Add((GameTag)tempTagID, new CardTag(curSubNode));
						}
						break;
					case "MasterPower":
						MasterPower = curSubNode.Value;
						break;
					case "Power":
						Dictionary<PlayReq, string> tempPower = new Dictionary<PlayReq, string>();
						foreach (XmlNode curPlayReq in curSubNode.ChildNodes) {
							if (curPlayReq.Name == "PlayRequirement") {
								int tempReqID = Convert.ToInt32(curPlayReq.Attributes["reqID"].Value);
								tempPower.Add((PlayReq)tempReqID, curPlayReq.Attributes["param"].Value);
							} else {
								throw new NotImplementedException("No Power sub-tag: " + curPlayReq.Name);
							}
						}
						Powers.Add(curSubNode.Attributes["definition"].Value, tempPower);
						break;
					//default:
					//	throw new NotImplementedException("Tag Not Implemented: " + curSubNode.Name);
				}
			}
		}

		/// <summary>
		/// Gets the abbreviated form on the card's description.
		/// </summary>
		public string ShortDescription {
			get {
				string returnStr = "";
				switch ((CardType)Tags[GameTag.CARDTYPE].Value) {
					case CardType.HERO:
						// return ("%s [%s][%s][%s %s][%d HP]%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Health, cardRace, flavorText)
						returnStr = string.Format("{0} [{1}][{2}][{3} {4}][{5} HP]",
												Tags[GameTag.CARDNAME]?.Value?["enUS"],
												CardID,
												Tags.ContainsKey(GameTag.CARD_SET) ? Tags[GameTag.CARD_SET].Value : (CardSet)0,
												Tags.ContainsKey(GameTag.CLASS) ? Tags[GameTag.CLASS].Value : (CardClass)0,
												Tags.ContainsKey(GameTag.CARDTYPE) ? Tags[GameTag.CARDTYPE].Value : (CardType)0,
												Tags.ContainsKey(GameTag.HEALTH) ? Tags[GameTag.HEALTH].Value : 0
							);
						if (Tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + Tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.MINION:
						// return ("%s [%s][%s][%s %s][%d mana, %d/%d]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.Atk, c.Health, c.CardTextInHand, cardRace, flavorText)
						returnStr = string.Format("{0} [{1}][{2}][{3} {4}][{5} mana, {6}/{7}]",
												Tags[GameTag.CARDNAME]?.Value?["enUS"],
												CardID,
												Tags.ContainsKey(GameTag.RARITY) ? Tags[GameTag.RARITY].Value : (Rarity)0,
												Tags.ContainsKey(GameTag.CLASS) ? Tags[GameTag.CLASS].Value : (CardClass)0,
												Tags.ContainsKey(GameTag.CARDTYPE) ? Tags[GameTag.CARDTYPE].Value : (CardType)0,
												Tags.ContainsKey(GameTag.COST) ? Tags[GameTag.COST].Value : 0,
												Tags.ContainsKey(GameTag.ATK) ? Tags[GameTag.ATK].Value : 0,
												Tags.ContainsKey(GameTag.HEALTH) ? Tags[GameTag.HEALTH].Value : 0
							);
						if (Tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + Tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.SPELL:
					case CardType.HERO_POWER:
						// return ("%s [%s][%s][%s %s][%d mana]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.CardTextInHand, cardRace, flavorText)
						returnStr = string.Format("{0} [{1}][{2}][{3} {4}][{5} mana]",
												Tags[GameTag.CARDNAME]?.Value?["enUS"],
												CardID,
												Tags.ContainsKey(GameTag.CARD_SET) ? Tags[GameTag.CARD_SET].Value : (CardSet)0,
												Tags.ContainsKey(GameTag.CLASS) ? Tags[GameTag.CLASS].Value : (CardClass)0,
												Tags.ContainsKey(GameTag.CARDTYPE) ? Tags[GameTag.CARDTYPE].Value : (CardType)0,
												Tags.ContainsKey(GameTag.COST) ? Tags[GameTag.COST].Value : 0
							);
						if (Tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + Tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.ENCHANTMENT:
						// return ("%s [%s][%s][%s %s]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.CardTextInHand, cardRace, flavorText)
						returnStr = string.Format("{0} [{1}][{2}][{3} {4}]",
												Tags[GameTag.CARDNAME]?.Value?["enUS"],
												CardID,
												Tags.ContainsKey(GameTag.CARD_SET) ? Tags[GameTag.CARD_SET].Value : (CardSet)0,
												Tags.ContainsKey(GameTag.CLASS) ? Tags[GameTag.CLASS].Value : (CardClass)0,
												Tags.ContainsKey(GameTag.CARDTYPE) ? Tags[GameTag.CARDTYPE].Value : (CardType)0
							);
						if (Tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + Tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.WEAPON:
						// return ("%s [%s][%s][%s %s][%d mana, %d/%d]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.Atk, c.Durability, c.CardTextInHand, cardRace, flavorText)
						returnStr = string.Format("{0} [{1}][{2}][{3} {4}][{5} mana, {6}/{7}]",
												Tags[GameTag.CARDNAME]?.Value?["enUS"],
												CardID,
												Tags.ContainsKey(GameTag.CARD_SET) ? Tags[GameTag.CARD_SET].Value : (CardSet)0,
												Tags.ContainsKey(GameTag.CLASS) ? Tags[GameTag.CLASS].Value : (CardClass)0,
												Tags.ContainsKey(GameTag.CARDTYPE) ? Tags[GameTag.CARDTYPE].Value : (CardType)0,
												Tags.ContainsKey(GameTag.COST) ? Tags[GameTag.COST].Value : 0,
												Tags.ContainsKey(GameTag.ATK) ? Tags[GameTag.ATK].Value : 0,
												Tags.ContainsKey(GameTag.DURABILITY) ? Tags[GameTag.DURABILITY].Value : 0
							);
						if (Tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + Tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
				}
				return returnStr;
			}
		}
	}
}