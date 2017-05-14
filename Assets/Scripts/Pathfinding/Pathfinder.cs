using UnityEngine;
using System.Collections.Generic;
using Utils.Linq;

namespace Pathfinding {
	public class Pathfinder {
		private Grid _grid;

		public Pathfinder(Grid grid) {
			_grid = grid;
		}

		public Vector3[] FindPath(Vector3 start, Vector3 target) {
			Node startNode = _grid.WorldPointToNode(start);
			Node targetNode = _grid.WorldPointToNode(target);

			if (!startNode.Walkable || !targetNode.Walkable) {
				return new Vector3[0];
			}

			return SimplifyPath(FindPath(startNode, targetNode));
		}

		private List<Node> FindPath(Node startNode, Node targetNode) {
			Heap<Node> openSet = new Heap<Node>(_grid.Size);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode) {
					return RetracePath(startNode, targetNode); ;
				}

				List<Node> neighbours = _grid.GetNeighbours(currentNode);
				foreach (Node neighbour in neighbours) {
					if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
						continue;
					}

					int cost = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (cost < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = cost;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains(neighbour)) {
							openSet.Add(neighbour);
						}
					}
				}
			}

			return new List<Node>();
		}

		private Vector3[] SimplifyPath(List<Node> path) {
			List<Vector3> waypoints = new List<Vector3>();
			Vector2 directionOld = Vector2.zero;

			for (int i = 1; i < path.Count; i++) {
				Vector2 directionNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);
				if (directionNew != directionOld || i == path.Count - 1) {
					waypoints.Add(path[i].WorldPosition);
				}
				directionOld = directionNew;
			}
			return waypoints.ToArray();
		}

		private List<Node> RetracePath(Node start, Node target) {
			List<Node> path = new List<Node>();
			Node currentNode = target;

			while (currentNode != start) {
				path.Add(currentNode);
				currentNode = currentNode.parent;
			}
			path.Reverse();

			return path;
		}

		private int GetDistance(Node a, Node b) {
			int dstX = Mathf.Abs(a.GridX - b.GridX);
			int dstY = Mathf.Abs(a.GridY - b.GridY);

			if (dstX > dstY) {
				return 14 * dstY + 10 * (dstX - dstY);
			}
			return 14 * dstX + 10 * (dstY - dstX);
		}
	}
}
