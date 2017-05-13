using UnityEngine;
using Pathfinding;

public partial class GameManager : MonoBehaviour {
	private Grid _grid;
	private Pathfinder _pathfinder;
	void Awake() {
		_grid = FindObjectOfType<Grid>();
	}

	void Start() {
		_pathfinder = new Pathfinder(_grid);
	}
}
