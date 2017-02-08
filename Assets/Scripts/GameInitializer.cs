using UnityEngine;
using Lua;

public class GameInitializer : MonoBehaviour {
	void Start() {
		LuaResources.LoadAll();

		DataSerializer.DeserializeData<TowerBase>(Application.streamingAssetsPath + "/Towers/ArrowTower/Tower.json");
	}
}
