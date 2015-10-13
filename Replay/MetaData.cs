// <copyright file="MetaData.cs" company="SpectralCoding.com">
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HSCSReader.Replay {
	internal class MetaData {
		private Game _game;
		public Int32 Meta;
		public Int32 Data;
		public Int32 Info;
		public String Ts;

		public MetaData(XmlNode xmlNode, Game game) {
			// meta NMTOKEN #REQUIRED
			// data % entity; #IMPLIED
			// info NMTOKEN #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			Int32.TryParse(xmlNode.Attributes?["meta"]?.Value, out Meta);
			Int32.TryParse(xmlNode.Attributes?["data"]?.Value, out Data);
			Int32.TryParse(xmlNode.Attributes?["info"]?.Value, out Info);
			Ts = xmlNode.Attributes?["ts"]?.Value;
		}
	}
}