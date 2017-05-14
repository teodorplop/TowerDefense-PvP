using UnityEngine;

public partial class GameManager {
	private class SellTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is SellTowerRequest;
		}

		public override void Execute() {
			SellTowerRequest request = _request as SellTowerRequest;
			Instance._towerFactory.DestroyTower(request.Tower);
		}
	}
}
