using UnityEngine;

public partial class GameManager {
	private class SellTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is SellTowerRequest;
		}

		public override void Execute() {
			SellTowerRequest request = _request as SellTowerRequest;
			Instance._uiManager.ShowUpgrades(null);

			Player player = Instance.GetPlayer(request.Player);
			player.TowerFactory.DestroyTower(player, request.Tower);
		}
	}
}
