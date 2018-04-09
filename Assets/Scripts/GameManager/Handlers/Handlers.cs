using UnityEngine;
using Ingame.towers;

public partial class GameManager {
	private class SellTowerHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is SellTowerRequest;
		}

		public override void Execute() {
			SellTowerRequest request = _request as SellTowerRequest;
			_instance._uiManager.ShowTower(null, null);

			Player player = Players.GetPlayer(request.Player);
			player.Wallet.Add(Wallet.Currency.Gold, _instance._towerFactory.GetSellCost(player, request.Tower));
			_instance._towerFactory.DestroyTower(player, request.Tower);

			EventManager.Raise(new SellTowerEvent(request));

			_instance._uiManager.Refresh();

			_instance.SetState(GameState.Idle);
		}
	}

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
				if (tower is BarracksTower)
					(tower as BarracksTower).Inject(_instance._unitFactory);

				EventManager.Raise(new UpgradeTowerEvent(request));

				_instance._uiManager.Refresh();
				_instance._uiManager.ShowTower(null, null);

				_instance.SetState(GameState.Idle);
			} else {
				Debug.Log("Not enough gold.");
			}
		}
	}

	private class SendMonstersHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is SendMonstersRequest;
		}

		public override void Execute() {
			_instance.SelectSendMonsters();
			_instance.SetState(GameState.Idle);
		}
	}

	private class SendMonsterHandler : ActionHandler {
		public override bool CanHandle(ActionRequest request) {
			return request is SendMonsterRequest;
		}

		public override void Execute() {
			SendMonsterRequest request = _request as SendMonsterRequest;

			Player player = Players.GetPlayer(request.PlayerOwner);
			MonsterToSend monster = request.Monster;

			if (player.Wallet.Check(Wallet.Currency.Gold, monster.cost)) {
				player.Wallet.Subtract(Wallet.Currency.Gold, monster.cost);
				_instance._uiManager.Refresh();

				string path = _instance._pathsContainer.Paths[0];
				foreach (Player target in Players.GetPlayers())
					if (target != player)
						_instance._wavesManager.QueueMonster(target, monster.name, path);
			} else {
				Debug.Log("Not enough gold.");
			}
		}
	}
}
