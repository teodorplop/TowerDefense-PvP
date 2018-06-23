using System;
using UnityEngine;

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

	void Awake() {
		startingTowers = startingStrategies[UnityEngine.Random.Range(0, startingStrategies.Length)].towerPlacements;
	}

	public static void GenerateAI(Player player) {
		AIPlayer ai = Instantiate(Resources.Load<AIPlayer>(string.Format("AI/AIPlayer_{0}", SceneLoader.ActiveScene)));
		ai.name = string.Format("AIPlayer_{0}", player.Name);
		ai.player = player;
	}
	public static string GenerateAIName() {
		return aiNames[UnityEngine.Random.Range(0, aiNames.Length)];
	}

	// hardcoded ugly stuff for the AI
	private float timer = 0.0f;
	void Update() {
		timer += Time.deltaTime;

		if (timer >= 1.0f) {
			DoStuff();
			timer = 0.0f;
		}
	}

	private int startingTowerIndex = 0;
	private void DoStuff() {
		if (startingTowerIndex < startingTowers.Length) {
			PlaceStartingTower();
		} else {

		}
	}

	private void PlaceStartingTower() {
		GameManager.Instance.HandleRequest(new UpgradeTowerRequest(player.Id, startingTowers[startingTowerIndex].towerSpot, startingTowers[startingTowerIndex].towerName));
		++startingTowerIndex;
	}
}
