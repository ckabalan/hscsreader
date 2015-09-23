using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.HSEnumerations {
	enum Zone {
		INVALID = 0,
		PLAY = 1,
		DECK = 2,
		HAND = 3,
		GRAVEYARD = 4,
		REMOVEDFROMGAME = 5,
		SETASIDE = 6,
		SECRET = 7,
		// Not public,
		DISCARD = -2,
	}
}