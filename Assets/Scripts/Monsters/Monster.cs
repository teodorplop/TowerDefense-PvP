using UnityEngine;

public partial class Monster : StateMachineBase {
	public enum MonsterState { Idle, Walking, Destination, Dead }

	[SerializeField]
	private int _maxHealth = 100;
	[SerializeField]
	private bool _debug;

	public int MaxHealth { get { return _maxHealth; } }
	public bool DebugOn { get { return _debug; } }
	private int _currentHealth;
	public int CurrentHealth { get { return _currentHealth; } }

	private int _pathIndex;
	private Vector3[] _path;
	public Player owner;

	void Start() {
		_currentHealth = _maxHealth;

		SetState(MonsterState.Idle);
	}

	public void SetPath(Vector3[] path) {
		_path = path;
		transform.position = _path[0] + owner.WorldOffset;
		transform.LookAt(_path[1] + owner.WorldOffset);
		_pathIndex = 1;
	}

	private void SetState(MonsterState state) {
		if (currentState == null || (MonsterState)currentState != state) {
			_stateMachineHandler.SetState(state, this);
		}
	}
}
