using Utils.Linq;
using UnityEngine;

public class MonsterFactory : MonoBehaviour {
	[SerializeField]
	private Monster[] _monsterPrefabs;
	[SerializeField]
	private UnitUI _monsterUIPrefab;

	private PathsContainer _pathsContainer;

	void Awake() {
		_pathsContainer = FindObjectOfType<PathsContainer>();
	}

	public Monster SendMonster(Player player, string monsterName, int count, string path) {
		Monster prefab = _monsterPrefabs.Find(obj => obj.name == monsterName);
		if (prefab == null) {
			Debug.LogError("Monster " + monsterName + " not found.");
			return null;
		}

		Monster monster = Instantiate(prefab);
		monster.transform.SetParent(player.Transform);
		monster.transform.localScale = Vector3.one;
		player.Register(monster);
		monster.SetPath(_pathsContainer.GetPath(path));

		UnitUI monsterUI = Instantiate(_monsterUIPrefab);
		monsterUI.transform.SetParent(player.Transform);
		monsterUI.transform.localScale = _monsterUIPrefab.transform.localScale;
		monsterUI.Inject(monster);

		return monster;
	}
}
