using UnityEngine;
using Utils.Linq;

namespace Pathfinding {
	public class PathfinderDebug : MonoBehaviour {
		public enum DebugType { Waypoints, Penalties }
		[SerializeField]
		private float _nodeRadius;
		[SerializeField]
		private int _gridBlurSize;
		[SerializeField]
		private DebugType _debugType;

		private TerrainInfo _terrainInfo;
		private Transform _start, _destination;
		private Grid _grid;
		private Pathfinder _pathfinder;

		private int _minPenalty, _maxPenalty;

		void Awake() {
			_terrainInfo = FindObjectOfType<TerrainInfo>();
			_start = transform.FindChild("Start");
			_destination = transform.FindChild("Destination");
		}

		void Start() {
			_grid = new Grid(_nodeRadius, _gridBlurSize, _terrainInfo);
			_pathfinder = new Pathfinder(_grid);

			_minPenalty = _maxPenalty = _grid.NodeGrid[0, 0].movementPenalty;
			foreach (Node node in _grid.NodeGrid) {
				_minPenalty = Mathf.Min(_minPenalty, node.movementPenalty);
				_maxPenalty = Mathf.Max(_maxPenalty, node.movementPenalty);
			}
		}

		void OnDrawGizmos() {
			if (_pathfinder != null) {
				switch (_debugType) {
					case DebugType.Waypoints:
						WaypointsDebug();
						break;
					case DebugType.Penalties:
						PenaltiesDebug();
						break;
				}
			}
		}

		private void WaypointsDebug() {
			Vector3[] path = _pathfinder.FindPath(_start.transform.position, _destination.transform.position);
			foreach (Node node in _grid.NodeGrid) {
				if (!node.Walkable) {
					Gizmos.color = Color.red;
				} else {
					Gizmos.color = path.Contains(node.WorldPosition) ? Color.black : Color.white;
				}
				Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeRadius * 2 - 0.2f));
			}
		}

		private void PenaltiesDebug() {
			foreach (Node node in _grid.NodeGrid) {
				if (!node.Walkable) {
					Gizmos.color = Color.red;
				} else {
					Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(_minPenalty, _maxPenalty, node.movementPenalty));
				}
				Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeRadius * 2 - 0.2f));
			}
		}
	}
}