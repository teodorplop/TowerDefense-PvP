using UnityEngine;

namespace Pathfinding {
	public class Grid : MonoBehaviour {
		[SerializeField]
		private float _nodeRadius;

		private float _nodeDiameter;

		private Rect _worldRectangle;
		private int _gridSizeX, _gridSizeY;
		private Node[,] _grid;
		public Node[,] Nodes { get { return _grid; } }

		void Awake() {
			_nodeDiameter = _nodeRadius * 2;
		}

		public void CreateGrid(TerrainInfo terrain) {
			_worldRectangle = terrain.WorldRectangle;
			Vector3 worldBottomLeft = new Vector3(_worldRectangle.xMin, 0, _worldRectangle.yMin);
			_gridSizeX = Mathf.RoundToInt(_worldRectangle.size.x / _nodeDiameter);
			_gridSizeY = Mathf.RoundToInt(_worldRectangle.size.y / _nodeDiameter);
			_grid = new Node[_gridSizeX, _gridSizeY];

			for (int x = 0; x < _gridSizeX; ++x) {
				for (int y = 0; y < _gridSizeY; ++y) {
					float rightOffset = x * _nodeDiameter + _nodeRadius;
					float forwardOffset = y * _nodeDiameter + _nodeRadius;
					Vector3 worldPoint = worldBottomLeft + new Vector3(rightOffset, 0, forwardOffset);

					_grid[x, y] = new Node(terrain.IsWalkable(worldPoint), worldPoint);
				}
			}
		}

		public Node WorldPointToNode(Vector3 worldPoint) {
			float percentX = (worldPoint.x - _worldRectangle.xMin) / _worldRectangle.size.x;
			float percentY = (worldPoint.y + _worldRectangle.yMin) / _worldRectangle.size.y;
			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);

			int x = Mathf.RoundToInt(percentX * (_gridSizeX - 1));
			int y = Mathf.RoundToInt(percentY * (_gridSizeY - 1));

			return _grid[x, y];
		}

		/*void OnDrawGizmos() {
			if (_grid != null) {
				foreach (Node n in _grid) {
					Gizmos.color = n.walkable ? Color.white : Color.red;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
				}
			}
		}*/
	}
}
