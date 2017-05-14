﻿using UnityEngine;

public partial class GameManager {
	private class UpgradeTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is UpgradeTowerRequest;
		}

		public override void Execute() {
			UpgradeTowerRequest request = _request as UpgradeTowerRequest;
			Instance._uiManager.ShowUpgrades(null);

			Player player = Instance.GetPlayer(request.Player);
			player.TowerFactory.UpgradeTower(player, request.Tower, request.Upgrade);
		}
	}
}
