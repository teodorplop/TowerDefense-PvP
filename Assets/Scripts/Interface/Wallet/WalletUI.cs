using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface.wallet {
	public class WalletUI : MonoBehaviour {
		[SerializeField]
		private IntLabel _healthLabel;
		[SerializeField]
		private IntLabel _goldLabel;

		private Wallet _wallet;

		public void Inject(Wallet wallet) {
			_wallet = wallet;
			Refresh();
		}

		public void Refresh() {
			_healthLabel.value = _wallet.Get(Wallet.Currency.Health);
			_goldLabel.value = _wallet.Get(Wallet.Currency.Gold);
		}
	}
}
