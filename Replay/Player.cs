using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	class Player {
		private Game _game;

		public Player(XmlNode xmlNode, Game game) {
			_game = game;
		}

	}
}
