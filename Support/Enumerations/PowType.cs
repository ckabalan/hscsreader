using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.Enumerations {
	enum PowType {
		FULL_ENTITY = 1,
		SHOW_ENTITY = 2,
		HIDE_ENTITY = 3,
		TAG_CHANGE = 4,
		ACTION_START = 5,
		ACTION_END = 6,
		CREATE_GAME = 7,
		META_DATA = 8,
	}
}
