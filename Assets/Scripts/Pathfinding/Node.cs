using UnityEngine;
using System;

namespace Pathfinding {
	public class Node : IHeapItem<Node> {
		private int _gridX, _gridY;
		private bool _walkable;
		private Vector3 _worldPosition;

		public int GridX { get { return _gridX; } }
		public int GridY { get { return _gridY; } }
		public bool Walkable { get { return _walkable; } }
		public Vector3 WorldPosition { get { return _worldPosition; } }

		public Node(int x, int y, bool walkable, Vector3 worldPosition) {
			_gridX = x;
			_gridY = y;
			_walkable = walkable;
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
