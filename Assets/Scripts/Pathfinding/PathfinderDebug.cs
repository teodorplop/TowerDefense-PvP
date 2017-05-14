using UnityEngine;
using Pathfinding;
using Utils.Linq;

public class PathfinderDebug : MonoBehaviour {
	[SerializeField]
	private float _nodeRadius;

	private TerrainInfo _terrainInfo;
	private Transform _start, _destination;
	private Grid _grid;
	private Pathfinder _pathfinder;

	void Awake() {
		_terrainInfo = FindObjectOfType<TerrainInfo>();
		_start = transform.FindChild("Start");
		_destination = transform.FindChild("Destination");
	}

	
	void Start() {
		_grid = new Grid(_nodeRadius, _terrainInfo);
		_pathfinder = new Pathfinder(_grid);
	}

	void OnDrawGizmos() {
		if (_pathfinder != null) {
			Vector3[] path = _pathfinder.FindPath(_start.transform.position, _destination.transform.position);
			foreach (Node node in _grid.NodeGrid) {
				if (!node.Walkable) {
					Gizmos.color = Color.red;
				} else {
					Gizmos.color = path.Contains(node.WorldPosition) ? Color.black : Color.white;
				}
				Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeRadius * 2));
			}
		}
	}
}
