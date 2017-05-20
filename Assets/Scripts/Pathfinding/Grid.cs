using UnityEngine;
using System.Collections.Generic;

namespace Pathfinding {
	public class Grid {
		private float _nodeRadius;
		private float _nodeDiameter;
		private int _blurSize;

		private Rect _worldRectangle;
		private int _gridSizeX, _gridSizeY;
		private Node[,] _grid;
		public Node[,] NodeGrid { get { return _grid; } }

		public int Size { get { return _gridSizeX * _gridSizeY; } }

		public Grid(float nodeRadius, int blurSize, TerrainInfo terrain) {
			_nodeRadius = nodeRadius;
			_nodeDiameter = nodeRadius * 2;
			_blurSize = blurSize;
			CreateGrid(terrain);
			BlurPenalties(_blurSize);
		}

		private void CreateGrid(TerrainInfo terrain) {
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

					_grid[x, y] = new Node(x, y, terrain.GetMovementPenalty(worldPoint), worldPoint);
				}
			}
		}

		private void BlurPenalties(int blurSize) {
			int kernelSize = blurSize * 2 + 1;
			int matrixSize = kernelSize * kernelSize;

			int[,] horizontalPass = BlurHorizontal(blurSize, kernelSize);
			int[,] verticalPass = BlurVertical(horizontalPass, blurSize, kernelSize);

			for (int x = 0; x < _gridSizeX; ++x) {
				for (int y = 0; y < _gridSizeY; ++y) {
					_grid[x, y].movementPenalty = Mathf.RoundToInt((float)verticalPass[x, y] / matrixSize);
				}
			}
		}

		private int[,] BlurHorizontal(int blurSize, int kernelSize) {
			int[,] horizontalPass = new int[_gridSizeX, _gridSizeY];
			for (int y = 0; y < _gridSizeY; ++y) {
				// Calculate first column value
				horizontalPass[0, y] = (blurSize + 1) * _grid[0, y].movementPenalty;
				for (int x = 1; x <= blurSize; ++x) {
					horizontalPass[0, y] += _grid[x, y].movementPenalty;
				}

				for (int x = 1; x < _gridSizeX; ++x) {
					int removeIndex = Mathf.Max(x - blurSize - 1, 0);
					int addIndex = Mathf.Min(x + blurSize, _gridSizeX - 1);
					horizontalPass[x, y] = horizontalPass[x - 1, y] - _grid[removeIndex, y].movementPenalty + _grid[addIndex, y].movementPenalty;
				}
			}

			return horizontalPass;
		}
		private int[,] BlurVertical(int[,] horizontalPass, int blurSize, int kernelSize) {
			int[,] verticalPass = new int[_gridSizeX, _gridSizeY];
			for (int x = 0; x < _gridSizeX; ++x) {
				// Calculate first row value
				verticalPass[x, 0] = (blurSize + 1) * horizontalPass[x, 0];
				for (int y = 1; y <= blurSize; ++y) {
					verticalPass[x, 0] += horizontalPass[x, y];
				}

				for (int y = 1; y < _gridSizeY; ++y) {
					int removeIndex = Mathf.Max(y - blurSize - 1, 0);
					int addIndex = Mathf.Min(y + blurSize, _gridSizeY - 1);
					verticalPass[x, y] = verticalPass[x, y - 1] - horizontalPass[x, removeIndex] + horizontalPass[x, addIndex];
				}
			}

			return verticalPass;
		}

		public Node WorldPointToNode(Vector3 worldPoint) {
			float percentX = (worldPoint.x - _worldRectangle.xMin) / _worldRectangle.size.x;
			float percentY = (worldPoint.z - _worldRectangle.yMin) / _worldRectangle.size.y;
			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);

			int x = Mathf.FloorToInt(percentX * (_gridSizeX - 1));
			int y = Mathf.FloorToInt(percentY * (_gridSizeY - 1));

			return _grid[x, y];
		}

		public List<Node> GetNeighbours(Node node) {
			int x = node.GridX, y = node.GridY;

			List<Node> neighbours = new List<Node>();
			for (int addX = -1; addX <= 1; ++addX) {
				for (int addY = -1; addY <= 1; ++addY) {
					if (addX == 0 && addY == 0) {
						continue;
					}

					int newX = x + addX, newY = y + addY;

					if (newX >= 0 && newX < _gridSizeX && newY >= 0 && newY < _gridSizeY) {
						neighbours.Add(_grid[newX, newY]);
					}
				}
			}

			return neighbours;
		}
	}
}
