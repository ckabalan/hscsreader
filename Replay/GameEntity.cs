using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	class GameEntity {
		private Game _game;
		public Int32 Id;
		public List<object> Children = new List<object>();

		public GameEntity(XmlNode xmlNode, Game game) {
			// id %entity; #REQUIRED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["id"]?.Value, out Id);
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeProcessor.Process(childNode, game));
			}
		}
	}
}
