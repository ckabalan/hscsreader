using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	class MetaData {
		private Game _game;

		public MetaData(XmlNode xmlNode, Game game) {
			_game = game;
		}
	}
}
