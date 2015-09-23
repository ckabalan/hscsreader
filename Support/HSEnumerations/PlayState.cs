using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.HSEnumerations {
	enum PlayState {
		INVALID = 0,
		PLAYING = 1,
		WINNING = 2,
		LOSING = 3,
		WON = 4,
		LOST = 5,
		TIED = 6,
		DISCONNECTED = 7,
		QUIT = 8,
	}
}
