using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.Enumerations {
	enum GoldRewardState {
		INVALID = 0,
		ELIGIBLE = 1,
		WRONG_GAME_TYPE = 2,
		ALREADY_CAPPED = 3,
		BAD_RATING = 4,
		SHORT_GAME = 5,
		OVER_CAIS = 6,
	}
}
