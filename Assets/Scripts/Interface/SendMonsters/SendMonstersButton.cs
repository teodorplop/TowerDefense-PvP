using UnityEngine;

namespace Interface.sendMonsters {
	public class SendMonstersButton : MonoBehaviour {
		public void OnPress() {
			GameManager.Instance.HandleRequest(new SendMonstersRequest());
		}
	}
}
