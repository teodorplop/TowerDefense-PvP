using UnityEngine;

namespace Pathfinding {
	public class Node : IHeapItem<Node> {
		private int _gridX, _gridY;
		// this needs to be public so it can be modified by the grid blur algorithm
		public int movementPenalty;
		private Vector3 _worldPosition;

		public int GridX { get { return _gridX; } }
		public int GridY { get { return _gridY; } }
		public bool Walkable { get { return movementPenalty != -1; } }
		public Vector3 WorldPosition { get { return _worldPosition; } }

		public Node(int x, int y, int movementPenalty, Vector3 worldPosition) {
			_gridX = x;
			_gridY = y;
			this.movementPenalty = movementPenalty;
			_worldPosition = worldPosition;
		}
		
		public int gCost;
		public int hCost;
		public int FCost { get { return gCost + hCost; } }
		public Node parent;

		private int _heapIndex;
		public int HeapIndex {
			get { return _heapIndex; }
			set { _heapIndex = value; }
		}

		public int CompareTo(Node other) {
			int compare = FCost.CompareTo(other.FCost);
			if (compare == 0) {
				compare = hCost.CompareTo(other.hCost);
			}
			return -compare;
		}
	}
}
