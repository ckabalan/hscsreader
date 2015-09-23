using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Support {
	public static class Helpers {
		public static String GameTagValueToString(GameTag gameTagType, Int32 enumValue) {
			if (Enum.IsDefined(typeof(GameTag), enumValue)) {
				switch (gameTagType) {
					case GameTag.CARDTYPE: return ((CardType)enumValue).ToString();
					case GameTag.RARITY: return ((Rarity)enumValue).ToString();
					case GameTag.ZONE: return ((Zone)enumValue).ToString();
				}
				return enumValue.ToString();
			}
			return enumValue.ToString();
		}
	}
}
