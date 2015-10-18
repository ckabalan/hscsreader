// <copyright file="Card.cs" company="SpectralCoding.com">
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
using System.Diagnostics;
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support.CardDefinitions {
	[DebuggerDisplay("{ShortDescription}")]
	public class Card {
		private readonly String _cardId;
		private readonly Dictionary<GameTag, CardTag> _tags = new Dictionary<GameTag, CardTag>();
		private String _masterPower;

		private Dictionary<String, Dictionary<PlayReq, String>> _powers =
			new Dictionary<String, Dictionary<PlayReq, String>>();

		private Int32 _version;

		/// <summary>
		/// Initializes the instance of the Card class.
		/// </summary>
		/// <param name="xmlNode">The XML Node which described the card.</param>
		public Card(XmlNode xmlNode) {
			_cardId = xmlNode.Attributes?["CardID"]?.Value;
			_version = Convert.ToInt32(xmlNode.Attributes?["version"]?.Value);
			foreach (XmlNode curSubNode in xmlNode.ChildNodes) {
				switch (curSubNode.Name) {
					case "Tag":
						Int32 tempTagId = Convert.ToInt32(curSubNode.Attributes?["enumID"].Value);
						if (_tags.ContainsKey((GameTag)tempTagId)) {
							_tags[(GameTag)tempTagId] = new CardTag(curSubNode);
						} else {
							_tags.Add((GameTag)tempTagId, new CardTag(curSubNode));
						}
						break;
					case "MasterPower":
						_masterPower = curSubNode.Value;
						break;
					case "Power":
						Dictionary<PlayReq, String> tempPower = new Dictionary<PlayReq, String>();
						foreach (XmlNode curPlayReq in curSubNode.ChildNodes) {
							if (curPlayReq.Name == "PlayRequirement") {
								Int32 tempReqId = Convert.ToInt32(curPlayReq.Attributes?["reqID"].Value);
								tempPower.Add((PlayReq)tempReqId, curPlayReq.Attributes?["param"].Value);
							} else {
								throw new NotImplementedException("No Power sub-tag: " + curPlayReq.Name);
							}
						}
						String value = curSubNode.Attributes?["definition"].Value;
						if (value != null) {
							_powers.Add(value, tempPower);
						} else {
							throw new NotSupportedException();
						}
						break;
					//default:
					//	throw new NotImplementedException("Tag Not Implemented: " + curSubNode.Name);
				}
			}
		}

		/// <summary>
		/// Gets the abbreviated form on the card's description.
		/// </summary>
		public String ShortDescription {
			get {
				String returnStr = "";
				switch ((CardType)_tags[GameTag.CARDTYPE].Value) {
					case CardType.HERO:
						// return ("%s [%s][%s][%s %s][%d HP]%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Health, cardRace, flavorText)
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} HP]",
												_tags[GameTag.CARDNAME]?.Value?["enUS"],
												_cardId,
												_tags.ContainsKey(GameTag.CARD_SET) ? _tags[GameTag.CARD_SET].Value : (CardSet)0,
												_tags.ContainsKey(GameTag.CLASS) ? _tags[GameTag.CLASS].Value : (CardClass)0,
												_tags.ContainsKey(GameTag.CARDTYPE) ? _tags[GameTag.CARDTYPE].Value : (CardType)0,
												_tags.ContainsKey(GameTag.HEALTH) ? _tags[GameTag.HEALTH].Value : 0
							);
						if (_tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + _tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.MINION:
						// return ("%s [%s][%s][%s %s][%d mana, %d/%d]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.Atk, c.Health, c.CardTextInHand, cardRace, flavorText)
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} mana, {6}/{7}]",
												_tags[GameTag.CARDNAME]?.Value?["enUS"],
												_cardId,
												_tags.ContainsKey(GameTag.RARITY) ? _tags[GameTag.RARITY].Value : (Rarity)0,
												_tags.ContainsKey(GameTag.CLASS) ? _tags[GameTag.CLASS].Value : (CardClass)0,
												_tags.ContainsKey(GameTag.CARDTYPE) ? _tags[GameTag.CARDTYPE].Value : (CardType)0,
												_tags.ContainsKey(GameTag.COST) ? _tags[GameTag.COST].Value : 0,
												_tags.ContainsKey(GameTag.ATK) ? _tags[GameTag.ATK].Value : 0,
												_tags.ContainsKey(GameTag.HEALTH) ? _tags[GameTag.HEALTH].Value : 0
							);
						if (_tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + _tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.SPELL:
					case CardType.HERO_POWER:
						// return ("%s [%s][%s][%s %s][%d mana]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.CardTextInHand, cardRace, flavorText)
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} mana]",
												_tags[GameTag.CARDNAME]?.Value?["enUS"],
												_cardId,
												_tags.ContainsKey(GameTag.CARD_SET) ? _tags[GameTag.CARD_SET].Value : (CardSet)0,
												_tags.ContainsKey(GameTag.CLASS) ? _tags[GameTag.CLASS].Value : (CardClass)0,
												_tags.ContainsKey(GameTag.CARDTYPE) ? _tags[GameTag.CARDTYPE].Value : (CardType)0,
												_tags.ContainsKey(GameTag.COST) ? _tags[GameTag.COST].Value : 0
							);
						if (_tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + _tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.ENCHANTMENT:
						// return ("%s [%s][%s][%s %s]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.CardTextInHand, cardRace, flavorText)
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}]",
												_tags[GameTag.CARDNAME]?.Value?["enUS"],
												_cardId,
												_tags.ContainsKey(GameTag.CARD_SET) ? _tags[GameTag.CARD_SET].Value : (CardSet)0,
												_tags.ContainsKey(GameTag.CLASS) ? _tags[GameTag.CLASS].Value : (CardClass)0,
												_tags.ContainsKey(GameTag.CARDTYPE) ? _tags[GameTag.CARDTYPE].Value : (CardType)0
							);
						if (_tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + _tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
					case CardType.WEAPON:
						// return ("%s [%s][%s][%s %s][%d mana, %d/%d]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.Atk, c.Durability, c.CardTextInHand, cardRace, flavorText)
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} mana, {6}/{7}]",
												_tags[GameTag.CARDNAME]?.Value?["enUS"],
												_cardId,
												_tags.ContainsKey(GameTag.CARD_SET) ? _tags[GameTag.CARD_SET].Value : (CardSet)0,
												_tags.ContainsKey(GameTag.CLASS) ? _tags[GameTag.CLASS].Value : (CardClass)0,
												_tags.ContainsKey(GameTag.CARDTYPE) ? _tags[GameTag.CARDTYPE].Value : (CardType)0,
												_tags.ContainsKey(GameTag.COST) ? _tags[GameTag.COST].Value : 0,
												_tags.ContainsKey(GameTag.ATK) ? _tags[GameTag.ATK].Value : 0,
												_tags.ContainsKey(GameTag.DURABILITY) ? _tags[GameTag.DURABILITY].Value : 0
							);
						if (_tags.ContainsKey(GameTag.CARDTEXT_INHAND)) {
							returnStr += ": " + _tags[GameTag.CARDTEXT_INHAND].Value["enUS"].ToString().Replace("\n", " | ");
						}
						break;
				}
				return returnStr;
			}
		}
	}
}
