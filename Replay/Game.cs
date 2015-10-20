// <copyright file="Game.cs" company="SpectralCoding.com">
//     Copyright (c) 2015 SpectralCoding
// </copyright>
// <license>
// This file is part of HSCSReader.
// 
// HSCSReader is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// HSCSReader is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with HSCSReader.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <author>Caesar Kabalan</author>

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using HSCSReader.Support;
using NLog;

namespace HSCSReader.Replay {
	public class Game {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly List<LogNode> _nodes = new List<LogNode>();
		private readonly String _ts;
		public readonly Dictionary<Int32, EntityState> ActorStates = new Dictionary<Int32, EntityState>();
		public readonly Boolean IsNewGame;
		//public GameEntity GameEntityObj;
		public readonly String Md5Hash;

		/// <summary>
		/// Initializes a new instance of the Game class.
		/// </summary>
		/// <param name="gameNode">The XML Node describing the Game.</param>
		public Game(XmlNode gameNode) {
			Md5Hash = Helpers.GetMd5Hash(MD5.Create(), gameNode.OuterXml);
			Logger.Trace($"Calculated MD5 from Game XML: {Md5Hash}");
			//IsNewGame = Uploader.IsNewGame(Md5Hash);
			IsNewGame = true;
			if (IsNewGame) {
				Logger.Info("MD5 Hash did not exist, parsing game...");
				_ts = gameNode.Attributes?["ts"]?.Value;
				foreach (XmlNode childNode in gameNode.ChildNodes) {
					// Import all the Node XML into C# objects
					_nodes.Add(NodeImporter.Import(childNode, this));
				}
				foreach (LogNode curNode in _nodes) {
					// Process each node's data.
					curNode.Process();
				}
			} else {
				Logger.Info("MD5 Hash exists, skipping game...");
			}
		}
	}
}
