using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.DataStorage;
using HSCSReader.Support;
using HSCSReader.Support.HSEnumerations;
using NLog;

namespace HSCSReader.Replay {
	public class Game {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		public String Ts;
		private List<object> _nodes = new List<object>();
		//public Dictionary<Int32, Entity> Entities = new Dictionary<Int32, Entity>();
		//public GameEntity GameEntityObj;
		public String Md5Hash;
		public Boolean IsNewGame = false;

		public Game(XmlNode gameNode) {
            Md5Hash = Helpers.GetMd5Hash(MD5.Create(), gameNode.OuterXml);
			Logger.Trace($"Calculated MD5 from Game XML: {Md5Hash}");
			//IsNewGame = Uploader.IsNewGame(Md5Hash);
			IsNewGame = true;
            if (IsNewGame) {
				Logger.Info($"MD5 Hash did not exist, parsing game...");
				Ts = gameNode.Attributes?["ts"]?.Value;
				foreach (XmlNode childNode in gameNode.ChildNodes) {
					NodeProcessor.Process(childNode, this);
				}
			} else {
				Logger.Info($"MD5 Hash exists, skipping game...");
			}
		}


	}
}
