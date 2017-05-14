using Utils.Linq;
using UnityEngine;

public class MonsterFactory : MonoBehaviour {
	[SerializeField]
	private Monster[] _monsterPrefabs;

	public Monster SendMonster(Player player, string monsterName, int count, string path) {
		Monster prefab = _monsterPrefabs.Find(obj => obj.name == monsterName);
		if (prefab == null) {
			Debug.LogError("Monster " + monsterName + " not found.");
			return null;
		}

		Monster monster = Instantiate(prefab);
		monster.transform.SetParent(transform);
		monster.transform.localScale = Vector3.one;
		monster.SetPath(player.PathsContainer.GetPath(path));
		player.Register(monster);

		return monster;
	}
}
