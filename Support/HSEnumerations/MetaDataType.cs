// <copyright file="MetaDataType.cs" company="SpectralCoding.com">
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
	internal enum MetaDataType {
		// From HistoryMeta.Type
		TARGET = 0,
		DAMAGE = 1,
		HEALING = 2,
		JOUST = 3,
		META_TARGET = TARGET,
		META_DAMAGE = DAMAGE,
		META_HEALING = HEALING
	}
}