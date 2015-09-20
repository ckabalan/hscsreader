using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.Enumerations {
	enum CardType {
		INVALID = 0,
		GAME = 1,
		PLAYER = 2,
		HERO = 3,
		MINION = 4,
		ABILITY = 5,
		ENCHANTMENT = 6,
		WEAPON = 7,
		ITEM = 8,
		TOKEN = 9,
		HERO_POWER = 10,
		SPELL = ABILITY,
	}
}
