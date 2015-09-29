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
		public String CardID;
		public Int32 Version;
		public Dictionary<GameTag, CardTag> Tags = new Dictionary<GameTag, CardTag>();
		public String MasterPower;
		public Dictionary<String, Dictionary<PlayReq, String>> Powers = new Dictionary<string, Dictionary<PlayReq, String>>();

		public String ShortDescription {
			get {


// if c.CardType == 3 then-- Hero
// return ("%s [%s][%s][%s %s][%d HP]%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Health, cardRace, flavorText)
// elseif c.CardType == 4 then-- Minion
// return ("%s [%s][%s][%s %s][%d mana, %d/%d]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.Atk, c.Health, c.CardTextInHand, cardRace, flavorText)
// elseif c.CardType == 5 or c.CardType == 10 then-- Spell or Hero Power
// return ("%s [%s][%s][%s %s][%d mana]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.CardTextInHand, cardRace, flavorText)
// elseif c.CardType == 6 then-- Enchantment
// return ("%s [%s][%s][%s %s]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.CardTextInHand, cardRace, flavorText)
// elseif c.CardType == 7 then-- Weapon
// return ("%s [%s][%s][%s %s][%d mana, %d/%d]: %s%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Cost, c.Atk, c.Durability, c.CardTextInHand, cardRace, flavorText)
// end

				String returnStr = "";
                switch ((CardType)Tags[GameTag.CARDTYPE].Value) {
					case CardType.HERO:
						// return ("%s [%s][%s][%s %s][%d HP]%s%s"):format(cardName, c.CardID, CardSet[c.CardSet], CardClass[c.Class], CardType[c.CardType], c.Health, cardRace, flavorText)
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} HP]",
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
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} mana, {6}/{7}]",
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
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} mana]",
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
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}]",
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
						returnStr = String.Format("{0} [{1}][{2}][{3} {4}][{5} mana, {6}/{7}]",
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

		public Card(XmlNode xmlNode) {
			CardID = xmlNode.Attributes?["CardID"]?.Value;
			Version = Convert.ToInt32(xmlNode.Attributes?["version"]?.Value);
			foreach (XmlNode curSubNode in xmlNode.ChildNodes) {
				switch (curSubNode.Name) {
					case "Tag":
						Int32 tempTagID = Convert.ToInt32(curSubNode.Attributes["enumID"].Value);
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
						Dictionary<PlayReq, String> tempPower = new Dictionary<PlayReq, String>();
						foreach (XmlNode curPlayReq in curSubNode.ChildNodes) {
							if (curPlayReq.Name == "PlayRequirement") {
								Int32 tempReqID = Convert.ToInt32(curPlayReq.Attributes["reqID"].Value);
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
	}
}
