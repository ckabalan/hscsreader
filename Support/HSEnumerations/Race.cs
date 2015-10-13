// <copyright file="Race.cs" company="SpectralCoding.com">
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
	internal enum Race {
		INVALID = 0,
		BLOODELF = 1,
		DRAENEI = 2,
		DWARF = 3,
		GNOME = 4,
		GOBLIN = 5,
		HUMAN = 6,
		NIGHTELF = 7,
		ORC = 8,
		TAUREN = 9,
		TROLL = 10,
		UNDEAD = 11,
		WORGEN = 12,
		GOBLIN2 = 13,
		MURLOC = 14,
		DEMON = 15,
		SCOURGE = 16,
		MECHANICAL = 17,
		ELEMENTAL = 18,
		OGRE = 19,
		PET = 20,
		TOTEM = 21,
		NERUBIAN = 22,
		PIRATE = 23,
		DRAGON = 24,
		// Alias for PET,
		BEAST = 20
	}
}