using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	class Tag {
		private Game _game;
		public Int32 Name;
		public Int32 Value;
		public Double Ts;

		public Tag(XmlNode xmlNode, Game game) {
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["tag"]?.Value, out Name);
			Int32.TryParse(xmlNode.Attributes?["value"]?.Value, out Value);
			Double.TryParse(xmlNode.Attributes?["ts"]?.Value, out Ts);
		}
	}
}
