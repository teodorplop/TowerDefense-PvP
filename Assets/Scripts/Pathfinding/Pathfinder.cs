using UnityEngine;
using System.Collections.Generic;
using System;

namespace Pathfinding {
	public class Pathfinder {
		private Grid _grid;

		public Pathfinder(Grid grid) {
			_grid = grid;
		}

		public Vector3 FindMinimumPenaltyPoint(Vector3 center, float range) {
			Node centerNode = _grid.WorldPointToNode(center);
			int gridRange = Mathf.CeilToInt(range / _grid.NodeDiameter);

			int xMin = Mathf.Max(0, centerNode.GridX - gridRange);
			int xMax = Mathf.Min(_grid.GridSizeX - 1, centerNode.GridX + gridRange);

			int yMin = Mathf.Max(0, centerNode.GridY - gridRange);
			int yMax = Mathf.Min(_grid.GridSizeY - 1, centerNode.GridY + gridRange);

			int minimumPenalty = int.MaxValue;
			Vector3 position = Vector3.zero;
			
			for (int x = xMin; x <= xMax; ++x) {
				for (int y = yMin; y <= yMax; ++y) {
					int penalty = _grid.NodeGrid[x, y].movementPenalty;
					Vector3 worldPosition = _grid.NodeGrid[x, y].WorldPosition;
					float distance = Vector3.Distance(center, worldPosition);

					if (penalty < minimumPenalty && distance <= range) {
						minimumPenalty = penalty;
						position = worldPosition;
					}
				}
			}

			return position;
		}

		public Vector3[] FindPath(Vector3 start, Vector3 target) {
			Node startNode = _grid.WorldPointToNode(start);
			Node targetNode = _grid.WorldPointToNode(target);

			if (!startNode.Walkable || !targetNode.Walkable) {
				return new Vector3[0];
			}

			bool success;
			return SimplifyPath(FindPath(startNode, targetNode, out success));
		}

		public void FindPath(Vector3 start, Vector3 target, Action<bool, Vector3[]> callback) {
			Node startNode = _grid.WorldPointToNode(start);
			Node targetNode = _grid.WorldPointToNode(target);

			if (!startNode.Walkable || !targetNode.Walkable) {
				if (callback != null) {
					callback(false, new Vector3[0]);
				}
				return;
			}

			bool success;
			Vector3[] path = SimplifyPath(FindPath(startNode, targetNode, out success));
			if (callback != null) {
				callback(success, path);
			}
		}

		private List<Node> FindPath(Node startNode, Node targetNode, out bool success) {
			Heap<Node> openSet = new Heap<Node>(_grid.Size);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode) {
					success = true;
					return RetracePath(startNode, targetNode);
				}

				List<Node> neighbours = _grid.GetNeighbours(currentNode);
				foreach (Node neighbour in neighbours) {
					if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
						continue;
					}

					int cost = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
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

			success = false;
			return new List<Node>();
		}

		private Vector3[] SimplifyPath(List<Node> path) {
			if (path.Count == 1) {
				return new Vector3[] { path[0].WorldPosition };
			}

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
