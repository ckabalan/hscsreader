// <copyright file="Tag.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay.LogNodes {
	internal class Tag : LogNode {
		private Game _game;
		public GameTag Name;
		public Int32 Value;
		public String Ts;

		public Tag(XmlNode xmlNode, Game game) {
			// tag % gameTag; #REQUIRED
			// value NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			if (xmlNode.Attributes?["tag"]?.Value == null) { throw new NullReferenceException(); }
			Name = (GameTag)Enum.Parse(typeof(GameTag), xmlNode.Attributes?["tag"]?.Value);
			Int32.TryParse(xmlNode.Attributes?["value"]?.Value, out Value);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}
	}
}