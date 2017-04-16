namespace GameEditor {
	public class PathDescriptionEditor : PathDescription {
		public bool AddPoint(Vector2i point) {
			if (!_points.Contains(point)) {
				_points.Add(point);
				return true;
			}
			return false;
		}

		public bool RemovePoint(Vector2i point) {
			return _points.Remove(point);
		}
	}
}
