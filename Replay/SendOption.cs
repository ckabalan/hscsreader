using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	class SendOption {
		private Game _game;

		public SendOption(XmlNode xmlNode, Game game) {
			_game = game;
		}

	}
}
