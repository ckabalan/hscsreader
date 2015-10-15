// <copyright file="PowSubType.cs" company="SpectralCoding.com">
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

namespace HSCSReader.Support.HSEnumerations {
	internal enum PowSubType {
		ATTACK = 1,
		JOUST = 2,
		POWER = 3,
		TRIGGER = 5,
		DEATHS = 6,
		PLAY = 7,
		FATIGUE = 8,
		// Removed
		SCRIPT = 4,
		ACTION = 99,
		// Renamed
		CONTINUOUS = 2
	}
}