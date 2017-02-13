using UnityEngine;

public class GameInitializer : MonoBehaviour {
	void Start() {
		GameResources.LoadAll();
		
		//Tower tower = new Tower(GameResources.Load<TowerBase>("Towers/ArrowTower/TowerBase"));
	}
}
