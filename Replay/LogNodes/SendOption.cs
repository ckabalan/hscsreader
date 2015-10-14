// <copyright file="SendOption.cs" company="SpectralCoding.com">
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
	internal class SendOption : LogNode {
		private Game _game;
		public Int32 Option;
		public Int32 SubOption;
		public Int32 Position;
		public Int32 Target;
		public String Ts;

		public SendOption(XmlNode xmlNode, Game game) {
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
	}
}