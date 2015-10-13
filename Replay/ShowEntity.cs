// <copyright file="ShowEntity.cs" company="SpectralCoding.com">
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
	internal class ShowEntity {
		private Game _game;
		public List<object> Children = new List<object>();
		private String cardId;
		private Int32 Entity;
		private String Ts;

		public ShowEntity(XmlNode xmlNode, Game game) {
			// cardID NMTOKEN #IMPLIED
			// entity % entity; #REQUIRED
			// ts NMTOKEN #IMPLIED
			_game = game;
			cardId = xmlNode.Attributes?["cardID"]?.Value;
			Int32.TryParse(xmlNode.Attributes?["entity"]?.Value, out Entity);
			Ts = xmlNode.Attributes?["ts"]?.Value;
			foreach (XmlNode childNode in xmlNode.ChildNodes) {
				Children.Add(NodeProcessor.Process(childNode, game));
			}
		}
	}
}