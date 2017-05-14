using UnityEngine;

public class Monster : MonoBehaviour {
	private int _pathIndex;
	private Vector3[] _path;
	public Player owner;

	public void SetPath(Vector3[] path) {
		_path = path;
		transform.position = _path[0] + owner.WorldOffset;
		_pathIndex = 0;
	}

	void FixedUpdate() {
		Vector3 position = transform.position - owner.WorldOffset;

		float distanceFromTarget = Vector3.Distance(position, _path[_pathIndex]);
		if (distanceFromTarget <= 1.0f) {
			++_pathIndex;

			if (_pathIndex < _path.Length) {
				PathRequestManager.RequestPath(owner, position, _path[_pathIndex], FindPathCallback);
			} else {
				Debug.Log("Reached destination");
			}
		}
	}

	private void FindPathCallback(bool success, Vector3[] waypoints) {

	}
}
