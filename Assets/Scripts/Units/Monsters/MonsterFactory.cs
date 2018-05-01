using Utils.Linq;
using UnityEngine;

public class MonsterFactory : MonoBehaviour {
	[SerializeField]
	private Monster[] _monsterPrefabs;
	private UnitAttributes[] _monsterAttributes;
	[SerializeField]
	private UnitUI _monsterUIPrefab;

	private PathsContainer _pathsContainer;

	void Awake() {
		_pathsContainer = FindObjectOfType<PathsContainer>();

		_monsterAttributes = new UnitAttributes[_monsterPrefabs.Length];
		for (int i = 0; i < _monsterPrefabs.Length; ++i) {
			_monsterAttributes[i] = GameResources.LoadMonsterAttributes(_monsterPrefabs[i].name);
		}
	}

	public Monster SendMonster(Player player, string monsterName, string path, Vector2 offset) {
		int idx = _monsterPrefabs.IndexOf(obj => obj.name == monsterName);
		if (idx == -1) {
			Debug.LogError("Monster " + monsterName + " not found.");
			return null;
		}

		Monster prefab = _monsterPrefabs[idx];
		UnitAttributes attributes = _monsterAttributes[idx];

		Monster monster = Instantiate(prefab);
		monster.transform.SetParent(player.Transform);
		monster.transform.localScale = Vector3.one;
		player.Register(monster);
		monster.SetAttributes(attributes);
		monster.SetPath(_pathsContainer.GetPath(path), offset);

		UnitUI monsterUI = Instantiate(_monsterUIPrefab);
		monsterUI.transform.SetParent(player.Transform);
		monsterUI.transform.localScale = _monsterUIPrefab.transform.localScale;
		monsterUI.Inject(monster);

		return monster;
	}
	
	public string GetMonsterUISprite(string name) {
		int idx = _monsterPrefabs.IndexOf(obj => obj.name == name);
		return idx != -1 ? _monsterAttributes[idx].uiSprite : "";
	}
}
