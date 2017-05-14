using UnityEngine;

public class Monster : MonoBehaviour {
	private int _pathIndex;
	private Vector3[] _path;
	public Player owner;

	public void SetPath(Vector3[] path) {
		_path = path;
		transform.position = _path[0];
		_pathIndex = 0;
	}

	void FixedUpdate() {
		float distanceFromTarget = Vector3.Distance(transform.position, _path[_pathIndex]);
		if (distanceFromTarget <= 1.0f) {
			++_pathIndex;

			if (_pathIndex < _path.Length) {
				//Vector3[] wayPoints = owner.Pathfinder.FindPath(transform.position, _path[_pathIndex]);
			} else {
				Debug.Log("Reached destination");
			}
		}
	}
}
