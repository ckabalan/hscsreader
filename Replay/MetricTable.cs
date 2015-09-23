using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSCSReader.Support.HSEnumerations;
using System.IO;

namespace HSCSReader.Replay {
	static class MetricTable {
		public static Dictionary<GameTag, Dictionary<dynamic, Dictionary<dynamic, String>>> Table = new Dictionary<GameTag, Dictionary<dynamic, Dictionary<dynamic, String>>>();



		public static void Load(String filePath) {
			// Populate the Table so we can do...
			//    Table[GameTag][OldSubtype][NewSubtype] = MetricName
			//    Example: Table[GameTag.ZONE][Zone.DECK][Zone.HAND] = "DeckToHandCount"
			using (StreamReader readFile = new StreamReader(filePath)) {
				String curLine;
				while ((curLine = readFile.ReadLine()) != null) {
					String[] column = curLine.Split(',');
					if (column.Length >= 4) {
						if (column[3] != String.Empty) {
							GameTag tempTag = (GameTag)Enum.Parse(typeof(GameTag), column[0].ToUpper());
							if (!Table.ContainsKey(tempTag)) {
								Table.Add(tempTag, new Dictionary<dynamic, Dictionary<dynamic, String>>());
							}

							dynamic oldValue;
							dynamic newValue;
							switch (tempTag) {
								case GameTag.ZONE:
									oldValue = (Zone)Enum.Parse(typeof(Zone), column[1]);
									newValue = (Zone)Enum.Parse(typeof(Zone), column[2]);
									break;
								default:
									return;
							}
							if (!Table[tempTag].ContainsKey(oldValue)) {
								Table[tempTag].Add(oldValue, new Dictionary<dynamic, String>());
							}
							if (!Table[tempTag][oldValue].ContainsKey(newValue)) {
								Table[tempTag][oldValue].Add(newValue, column[3]);
							}
						}
					}
				}
			}
		}
	}
}
