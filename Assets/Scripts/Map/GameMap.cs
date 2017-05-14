using System.Collections.Generic;
using UnityEngine;
using Utils.Linq;

public class GameMap {
	private List<Tower> _towers;
	private List<Monster> _monsters;

	public GameMap() {
		_towers = new List<Tower>();
		_monsters = new List<Monster>();
	}

	public List<Tower> GetTowersInRange(Vector3 position, float radius) {
		return _towers.Where(obj => Vector3.Distance(obj.transform.position, position) <= radius).ToList();
	}
	public List<Monster> GetMonstersInRange(Vector3 position, float radius) {
		return _monsters.Where(obj => Vector3.Distance(obj.transform.position, position) <= radius).ToList();
	}
}
