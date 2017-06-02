using UnityEngine;

public partial class Monster : BaseUnit {
	public enum MonsterState { Destination }

	public int GoldAwarded { get { return _attributes.awardedGold; } }
	public int LifeTaken { get { return _attributes.lifeTaken; } }

	private int _pathIndex;
	private Vector3[] _path;

	void Start() {
		SetState(BaseUnitState.Idle);
	}

	public void SetPath(Vector3[] path, Vector2 offset) {
		_path = path;
		transform.position = _path[0] + owner.WorldOffset + new Vector3(offset.x, 0, offset.y);
		transform.LookAt(_path[1] + owner.WorldOffset);
		_pathIndex = 1;
	}

	public override bool CanBeAttacked() {
		return !IsDead && !_reachedDestination;
	}
}
