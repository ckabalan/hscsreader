// <copyright file="CardSet.cs" company="SpectralCoding.com">
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

namespace HSCSReader.Support.HSEnumerations {
	internal enum CardSet {
		INVALID = 0,
		TEST_TEMPORARY = 1,
		CORE = 2,
		EXPERT1 = 3,
		REWARD = 4,
		MISSIONS = 5,
		DEMO = 6,
		NONE = 7,
		CHEAT = 8,
		BLANK = 9,
		DEBUG_SP = 10,
		PROMO = 11,
		FP1 = 12,
		PE1 = 13,
		BRM = 14,
		TGT = 15,
		CREDITS = 16,
		HERO_SKINS = 17,
		TB = 18,
		SLUSH = 19,
		FP2 = BRM,
		PE2 = TGT
	}
}