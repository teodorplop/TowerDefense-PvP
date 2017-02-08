using UnityEngine;
using Lua;

public class GameInitializer : MonoBehaviour {
	void Start() {
		LuaResources.LoadAll();
	}
}
