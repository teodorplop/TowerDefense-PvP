using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

public class UIPlayerStat : MonoBehaviour {
	[SerializeField] private Label nameLabel;
	[SerializeField] private IntLabel healthLabel;
	[SerializeField] private IntLabel goldLabel;

	private Wallet wallet;

	public void Inject(Player player) {
		this.wallet = player.Wallet;
		nameLabel.text = player.Name;
		Refresh();
	}

	public void Refresh() {
		if (wallet != null) {
			healthLabel.value = wallet.Get(Wallet.Currency.Health);
			goldLabel.value = wallet.Get(Wallet.Currency.Gold);
		}
	}

	/*void Update() {
		if (wallet != null) {
			healthLabel.value = wallet.Get(Wallet.Currency.Health);
			goldLabel.value = wallet.Get(Wallet.Currency.Gold); 
		}
	}*/
}
