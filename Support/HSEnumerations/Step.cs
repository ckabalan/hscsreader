// /// <copyright file="Step.cs" company="SpectralCoding.com">
// ///     Copyright (c) 2015 SpectralCoding
// /// </copyright>
// /// <license>
// /// This file is part of HSCSReader.
// ///
// /// HSCSReader is free software: you can redistribute it and/or modify
// /// it under the terms of the GNU General Public License as published by
// /// the Free Software Foundation, either version 3 of the License, or
// /// (at your option) any later version.
// ///
// /// HSCSReader is distributed in the hope that it will be useful,
// /// but WITHOUT ANY WARRANTY; without even the implied warranty of
// /// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// /// GNU General Public License for more details.
// ///
// /// You should have received a copy of the GNU General Public License
// /// along with HSCSReader.  If not, see <http://www.gnu.org/licenses/>.
// /// </license>
// /// <author>Caesar Kabalan</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSCSReader.Support.HSEnumerations {
	internal enum Step {
		INVALID = 0,
		BEGIN_FIRST = 1,
		BEGIN_SHUFFLE = 2,
		BEGIN_DRAW = 3,
		BEGIN_MULLIGAN = 4,
		MAIN_BEGIN = 5,
		MAIN_READY = 6,
		MAIN_RESOURCE = 7,
		MAIN_DRAW = 8,
		MAIN_START = 9,
		MAIN_ACTION = 10,
		MAIN_COMBAT = 11,
		MAIN_END = 12,
		MAIN_NEXT = 13,
		FINAL_WRAPUP = 14,
		FINAL_GAMEOVER = 15,
		MAIN_CLEANUP = 16,
		MAIN_START_TRIGGERS = 17
	}
}