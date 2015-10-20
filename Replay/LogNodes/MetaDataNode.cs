// <copyright file="MetaDataNode.cs" company="SpectralCoding.com">
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
	internal class MetaDataNode : LogNode {
		private readonly Game _game;
		public readonly Int32 Data;
		public readonly Int32 Info;
		public readonly MetaDataType Meta;
		public readonly String Ts;

		/// <summary>
		/// Initializes an instance of the MetaDataNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public MetaDataNode(XmlNode xmlNode, Game game) {
			// meta NMTOKEN #REQUIRED
			// data % entity; #IMPLIED
			// info NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			if (xmlNode.Attributes?["meta"]?.Value == null) { throw new NullReferenceException(); }
			Meta = (MetaDataType)Enum.Parse(typeof(MetaDataType), xmlNode.Attributes?["meta"]?.Value);
			Int32.TryParse(xmlNode.Attributes?["data"]?.Value, out Data);
			Int32.TryParse(xmlNode.Attributes?["info"]?.Value, out Info);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() { }
	}
}
