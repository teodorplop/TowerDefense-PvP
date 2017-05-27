using UnityEngine;

public partial class GameManager {
	private class SellTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is SellTowerRequest;
		}

		public override void Execute() {
			SellTowerRequest request = _request as SellTowerRequest;
			_instance._uiManager.ShowUpgrades(null);
			
			Player player = Players.GetPlayer(request.Player);
			_instance._towerFactory.DestroyTower(player, request.Tower);
			player.Wallet.Add(Wallet.Currency.Gold, _instance._towerFactory.GetSellCost(player, request.Tower));

			_instance._uiManager.Refresh();

			_instance.SetState(GameState.Idle);
		}
	}
}
