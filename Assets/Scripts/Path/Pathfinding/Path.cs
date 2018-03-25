using UnityEngine;

namespace Pathfinding {
	public class Path {
		public readonly Vector3[] waypoints;
		public readonly Line[] turnBoundaries;

		public Path(Vector3[] waypoints, Vector3 startPos, float turnDst) {
			this.waypoints = waypoints;
			turnBoundaries = new Line[waypoints.Length];

			Vector2 previousPoint = ToVector2(startPos);
			for (int i = 0; i < waypoints.Length; ++i) {
				Vector2 currentPoint = ToVector2(waypoints[i]);
				Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
				Vector2 turnBoundaryPoint = (i == waypoints.Length - 1) ? currentPoint : currentPoint - dirToCurrentPoint * turnDst;
				turnBoundaries[i] = new Line(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turnDst);
				previousPoint = turnBoundaryPoint;
			}
		}

		private Vector2 ToVector2(Vector3 v3) {
			return new Vector2(v3.x, v3.z);
		}
	}
}
