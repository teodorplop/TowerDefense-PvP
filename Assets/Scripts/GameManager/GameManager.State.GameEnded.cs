using System.Collections;
using UnityEngine;
using Utils.Linq;

public partial class GameManager {
	private Player _winner;

	public void ReachedDestination(Monster monster) {
		if (_winner != null) {
			return;
		}

		monster.owner.Wallet.Subtract(Wallet.Currency.Health, 1);

		if (monster.owner.Wallet.Get(Wallet.Currency.Health) == 0) {
			Debug.Log("Player " + monster.owner.Name + " died.");
			monster.owner.isActive = false;

			Player[] players = Players.GetPlayers();
			int activePlayers = players.Count(obj => obj.isActive);
			if (activePlayers == 1) {
				_winner = players.Find(obj => obj.isActive);
				SetState(GameState.GameEnded);
			}
		}
	}

	IEnumerator GameEnded_EnterState() {
		Debug.Log("Winner: " + _winner.Name);
		yield return null;
	}
}
