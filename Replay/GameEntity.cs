// /// <copyright file="GameEntity.cs" company="SpectralCoding.com">
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
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay {
	public class GameEntity : Entity {
		public GameEntity(XmlNode entityNode, Game game) : base(entityNode, game) { }

		public override void ChangeOrAddTag(Game game, GameTag tagToChange, int newValue, string timestamp = "") {
			int oldValue;
			if (Tags.ContainsKey(tagToChange)) {
				oldValue = Tags[tagToChange];
			} else {
				oldValue = -1;
			}
			base.ChangeOrAddTag(game, tagToChange, newValue, timestamp);

			switch (tagToChange) {
				case GameTag.STEP:
					if (oldValue == (int)Step.MAIN_NEXT) {
						if (newValue == (int)Step.MAIN_READY) {
							foreach (KeyValuePair<int, Entity> curEntKVP in game.Entities) { curEntKVP.Value.StartTurn(game, timestamp); }
						}
					} else if (oldValue == (int)Step.MAIN_END) {
						if (newValue == (int)Step.MAIN_CLEANUP) {
							foreach (KeyValuePair<int, Entity> curEntKVP in game.Entities) { curEntKVP.Value.EndTurn(game, timestamp); }
						}
					}
					break;
			}
		}
	}
}