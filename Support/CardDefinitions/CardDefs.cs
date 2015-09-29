using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Replay;

namespace HSCSReader.Support.CardDefinitions {
	public static class CardDefs {
		public static Dictionary<String, Card> Cards = new Dictionary<string, Card>();

		public static void Load(String filePath) {
			XmlDocument cardsDoc = new XmlDocument();
			cardsDoc.Load(filePath);
			XmlNode rootNode = cardsDoc.SelectSingleNode("/CardDefs");
			XmlNodeList entityNodeList = cardsDoc.SelectNodes("/CardDefs/Entity");
			foreach (XmlNode curEntityNode in entityNodeList) {
				Cards.Add(curEntityNode.Attributes["CardID"].Value, new Card(curEntityNode));
			}
			//foreach (KeyValuePair<String, Card> curCard in Cards) {
			//	Console.WriteLine(curCard.Value.ShortDescription);
			//}
		}
	}
}
