using UnityEngine;

public partial class Monster : BaseUnit {
	public enum MonsterState { Destination }

	private int _pathIndex;
	private Vector3[] _path;

	void Start() {
		SetState(BaseUnitState.Idle);
	}

	public void SetPath(Vector3[] path) {
		_path = path;
		transform.position = _path[0] + owner.WorldOffset;
		transform.LookAt(_path[1] + owner.WorldOffset);
		_pathIndex = 1;
	}

	public override bool CanBeAttacked() {
		return !IsDead && !_reachedDestination;
	}
}
