using System;
using System.Collections.Generic;
using UnityEngine;
using Ingame.towers;

public class AIPlayer : MonoBehaviour {
	private static string[] aiNames = {
		"manthrilos", "hacker913", "hetoxin", "GuineaPiglet", "mboog12", "Soulbane", "CleverDude", "XStomperX", "randomguy"
	};

	[Serializable]
	private struct StartingStrategy {
		public TowerPlacement[] towerPlacements;
	}
	[Serializable]
	private struct TowerPlacement {
		public string towerSpot;
		public string towerName;
	}

	[SerializeField] private StartingStrategy[] startingStrategies;

	private TowerPlacement[] startingTowers;
	private Player player;
	private TowerFactory towerFactory;
	private SendMonstersList sendMonsters;

	void Awake() {
		startingTowers = startingStrategies[UnityEngine.Random.Range(0, startingStrategies.Length)].towerPlacements;
	}

	public static void GenerateAI(Player player, TowerFactory towerFactory, SendMonstersList sendMonsters) {
		AIPlayer ai = Instantiate(Resources.Load<AIPlayer>(string.Format("AI/AIPlayer_{0}", SceneLoader.ActiveScene)));
		ai.name = string.Format("AIPlayer_{0}", player.Name);
		ai.player = player;
		ai.towerFactory = towerFactory;
		ai.sendMonsters = sendMonsters;
	}
	public static string GenerateAIName() {
		return aiNames[UnityEngine.Random.Range(0, aiNames.Length)];
	}

	// hardcoded ugly stuff for the AI
	private float timer = 0.0f;
	private float randomTimer = 1.5f;
	void Update() {
		timer += Time.deltaTime;

		if (timer >= 1.0f && startingTowerIndex < startingTowers.Length) {
			PlaceStartingTower();
			timer = 0.0f;
		} else if (timer >= randomTimer) {
			if (DoStuff()) {
				randomTimer = UnityEngine.Random.Range(2.0f, 4.0f);
			} else
				randomTimer = UnityEngine.Random.Range(1.0f, 2.0f);
			timer = 0.0f;
		}
	}

	private int startingTowerIndex = 0;
	private void PlaceStartingTower() {
		GameManager.Instance.HandleRequest(new UpgradeTowerRequest(player.Id, startingTowers[startingTowerIndex].towerSpot, startingTowers[startingTowerIndex].towerName));
		++startingTowerIndex;
	}

	private bool DoStuff() {
		if (UnityEngine.Random.Range(0, 4) == 0) return SendAttack();
		return UpgradeTower();
	}

	private bool SendAttack() {
		List<MonsterToSend> monsters = new List<MonsterToSend>(sendMonsters.Monsters);
		monsters.Shuffle();

		foreach (MonsterToSend monster in monsters) {
			if (player.Wallet.Check(Wallet.Currency.Gold, monster.cost)) {
				GameManager.Instance.HandleRequest(new SendMonsterRequest(player.Id, monster));
				return true;
			}
		}
		return false;
	}

	private bool UpgradeTower() {
		List<Tower> towers = new List<Tower>(player.Towers);
		towers.Shuffle();

		string[] upgrades;
		foreach (Tower tower in player.Towers) {
			upgrades = tower.Upgrades;
			upgrades.Shuffle();

			foreach (string upgrade in upgrades) {
				if (upgrade == "SellTower" || upgrade == "RallyPoint") continue;

				int cost = towerFactory.GetUpgradeCost(player, upgrade);
				if (player.Wallet.Check(Wallet.Currency.Gold, cost)) {
					GameManager.Instance.HandleRequest(new UpgradeTowerRequest(player.Id, tower.name, upgrade));
					return true;
				}
			}
		}

		return false;
	}
}
