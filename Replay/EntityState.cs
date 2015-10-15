// <copyright file="EntityState.cs" company="SpectralCoding.com">
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
using HSCSReader.Support.HSEnumerations;
using NLog;

namespace HSCSReader.Replay {
	public class EntityState {
		protected static Logger Logger = LogManager.GetCurrentClassLogger();
		public Dictionary<String, String> Attributes;
		public Int32 Id;
		public List<Metric> Metrics;
		public Dictionary<GameTag, Int32> Tags;

		public EntityState() {
			Tags = new Dictionary<GameTag, Int32>();
			Attributes = new Dictionary<String, String>();
			Metrics = new List<Metric>();
		}
	}
}