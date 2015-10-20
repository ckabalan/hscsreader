// <copyright file="SendOptionNode.cs" company="SpectralCoding.com">
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

namespace HSCSReader.Replay.LogNodes {
	internal class SendOptionNode : LogNode {
		private readonly Game _game;
		public readonly Int32 Option;
		public readonly Int32 Position;
		public readonly Int32 SubOption;
		public readonly Int32 Target;
		public readonly String Ts;

		/// <summary>
		/// Initializes an instance of the SendOptionNode class.
		/// </summary>
		/// <param name="xmlNode">The XML Node describing the Node.</param>
		/// <param name="game">The game object related to the Node.</param>
		public SendOptionNode(XmlNode xmlNode, Game game) {
			// option NMTOKEN #REQUIRED
			// subOption NMTOKEN #IMPLIED
			// position NMTOKEN #IMPLIED
			// target NMTOKEN #IMPLIED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["option"]?.Value, out Option);
			Int32.TryParse(xmlNode.Attributes?["subOption"]?.Value, out SubOption);
			Int32.TryParse(xmlNode.Attributes?["position"]?.Value, out Position);
			Int32.TryParse(xmlNode.Attributes?["target"]?.Value, out Target);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}

		/// <summary>
		/// Processes this node, deriving whatever information it can.
		/// </summary>
		public override void Process() { }
	}
}
