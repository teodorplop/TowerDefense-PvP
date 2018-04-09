using UnityEngine;

namespace Interface.sendMonsters {
	public class SendMonsterElement : MonoBehaviour {
		[SerializeField] private Label nameLabel;
		[SerializeField] private IntLabel costLabel;
		
		private MonsterToSend monster;

		public virtual void Inject(MonsterToSend monster) {
			this.monster = monster;
			nameLabel.text = monster.name;
			costLabel.value = monster.cost;
		}

		public virtual void OnPress() {
			GameManager.Instance.HandleRequest(new SendMonsterRequest(Players.ClientPlayer.Id, monster));
		}
	}
}
