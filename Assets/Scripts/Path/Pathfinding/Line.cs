using UnityEngine;

namespace Pathfinding {
	public struct Line {
		private static float _verticalLineGradient = 1e5f;

		private float _slope;
		private Vector2 _pointOnLine1, _pointOnLine2;
		private bool _initialSide;

		public Line(Vector2 point, Vector2 startPoint) {
			float dx = point.x - startPoint.x;
			float dy = point.y - startPoint.y;
			float perpendicularScope = dx == 0 ? _verticalLineGradient : dy / dx;

			_slope = perpendicularScope == 0 ? _verticalLineGradient : -1.0f / perpendicularScope;

			_pointOnLine1 = point;
			_pointOnLine2 = point + new Vector2(1, _slope);

			_initialSide = false;
			_initialSide = GetSide(startPoint);
		}

		bool GetSide(Vector2 p) {
			return (p.x - _pointOnLine1.x) * (_pointOnLine2.y - _pointOnLine1.y) > (p.y - _pointOnLine1.y) * (_pointOnLine2.x - _pointOnLine1.x);
		}

		public bool HasCrossedLine(Vector2 p) {
			return GetSide(p) != _initialSide;
		}
	}
}
