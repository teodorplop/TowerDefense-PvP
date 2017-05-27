using UnityEngine;

public partial class GameManager {
	private class UpgradeTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is UpgradeTowerRequest;
		}

		public override void Execute() {
			UpgradeTowerRequest request = _request as UpgradeTowerRequest;
			_instance._uiManager.ShowUpgrades(null);

			Player player = Players.GetPlayer(request.Player);
			_instance._towerFactory.UpgradeTower(player, request.Tower, request.Upgrade);

			_instance.SetState(GameState.Idle);
		}
	}
}
