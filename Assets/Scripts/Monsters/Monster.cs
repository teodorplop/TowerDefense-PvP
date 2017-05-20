using UnityEngine;

public partial class Monster : StateMachineBase {
	public enum MonsterState { Idle, Walking }

	[SerializeField]
	private bool _debug;

	private int _pathIndex;
	private Vector3[] _path;
	public Player owner;

	void Start() {
		SetState(MonsterState.Idle);
	}

	public void SetPath(Vector3[] path) {
		_path = path;
		transform.position = _path[0] + owner.WorldOffset;
		_pathIndex = 1;
	}

	private void SetState(MonsterState state) {
		if (currentState == null || (MonsterState)currentState != state) {
			_stateMachineHandler.SetState(state, this);
		}
	}
}
