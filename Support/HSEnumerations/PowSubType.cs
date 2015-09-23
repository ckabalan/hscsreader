using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.HSEnumerations {
	enum PowSubType {
		ATTACK = 1,
		JOUST = 2,
		POWER = 3,
		TRIGGER = 5,
		DEATHS = 6,
		PLAY = 7,
		FATIGUE = 8,
		// Removed
		SCRIPT = 4,
		ACTION = 99,
		// Renamed
		CONTINUOUS = 2,
	}
}
