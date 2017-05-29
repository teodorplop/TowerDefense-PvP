using UnityEngine;
using Ingame.towers;

public partial class GameManager {
	private class UpgradeTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is UpgradeTowerRequest;
		}

		public override void Execute() {
			UpgradeTowerRequest request = _request as UpgradeTowerRequest;

			Player player = Players.GetPlayer(request.Player);
			int cost = _instance._towerFactory.GetUpgradeCost(player, request.Upgrade);
			// TODO: What if the server player did this? do we still need this check?
			if (player.Wallet.Check(Wallet.Currency.Gold, cost)) {
				player.Wallet.Subtract(Wallet.Currency.Gold, cost);
				Tower tower = _instance._towerFactory.UpgradeTower(player, request.Tower, request.Upgrade, cost);

				if (tower is BarracksTower) {
					(tower as BarracksTower).Inject(_instance._unitFactory);
				}

				_instance._uiManager.Refresh();
				_instance._uiManager.ShowUpgrades(null, null);

				_instance.SetState(GameState.Idle);
			} else {
				Debug.Log("Not enough gold.");
			}
		}
	}
}
