using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HSCSReader.Support.HSEnumerations;

namespace HSCSReader.Replay {
	public class GameEntity : Entity {

		public GameEntity(XmlNode entityNode, Game game) : base(entityNode, game) {

		}

		public override void ChangeOrAddTag(Game game, GameTag tagToChange, Int32 newValue, String timestamp = "") {
			Int32 oldValue;
            if (base.Tags.ContainsKey(tagToChange)) {
				oldValue = base.Tags[tagToChange];
			} else {
				oldValue = -1;
			}
			base.ChangeOrAddTag(game, tagToChange, newValue, timestamp);

			switch (tagToChange) {
				case GameTag.STEP:
					if (oldValue == (Int32)Step.MAIN_NEXT) {
						if (newValue == (Int32)Step.MAIN_READY) {
							foreach (KeyValuePair<Int32, Entity> curEntKVP in game.Entities) { curEntKVP.Value.StartTurn(game, timestamp); }
						}
					} else if (oldValue == (Int32)Step.MAIN_END) {
						if (newValue == (Int32)Step.MAIN_CLEANUP) {
							foreach (KeyValuePair<Int32, Entity> curEntKVP in game.Entities) { curEntKVP.Value.EndTurn(game, timestamp); }
						}
					}
					break;
			}
		}
    }
}
