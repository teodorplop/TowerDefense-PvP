using UnityEngine;
using UnityEngine.UI;

namespace Interface.sendMonsters {
	public class SendMonsterElement : MonoBehaviour {
		[SerializeField] private Image texture;
		[SerializeField] private IntLabel costLabel;
		
		private MonsterToSend monster;

		public MonsterToSend Monster { get { return monster; } }

		public virtual void Inject(MonsterFactory monsterFactory, MonsterToSend monster) {
			this.monster = monster;

			texture.sprite = StreamingAssets.GetSprite(monster.name + "UI_Tex");
			costLabel.value = monster.cost;
		}

		public virtual void OnPress() {
			GameManager.Instance.HandleRequest(new SendMonsterRequest(Players.ClientPlayer.Id, monster));
		}
	}
}
