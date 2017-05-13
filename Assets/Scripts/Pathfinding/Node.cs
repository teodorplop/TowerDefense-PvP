using UnityEngine;

namespace Pathfinding {
	public class Node {
		private bool _walkable;
		private Vector3 _worldPosition;

		public bool Walkable { get { return _walkable; } }
		public Vector3 WorldPosition { get { return _worldPosition; } }
		
		public Node(bool walkable, Vector3 worldPosition) {
			_walkable = walkable;
			_worldPosition = worldPosition;
		}
	}
}
