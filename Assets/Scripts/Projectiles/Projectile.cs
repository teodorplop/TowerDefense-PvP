using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _turnSpeed;

	private bool _followTarget;
	private int _damage;
	private Monster _target;
	private Vector3 TargetPosition { get { return _target.transform.position + new Vector3(0, 0.5f); } }
	public void Inject(int damage, Monster monster) {
		_damage = damage;
		_target = monster;
		transform.LookAt(monster.transform.position);
		_followTarget = true;
	}

	private bool ReachedTarget() {
		return _target == null ? false : Vector3.Distance(TargetPosition, transform.position) <= 0.75f;
	}
	private bool TargetIsStillValid() {
		return _target != null && !_target.IsDead && !_target.ReachedDestination;
	}

	protected virtual void OnTargetReached(int damage, Monster target) {
	}

	void FixedUpdate() {
		if (!_followTarget) {
			// We do not have a target yet
			return;
		}

		if (!TargetIsStillValid()) {
			// TODO: just throw this projectile somewhere on the ground
			Destroy(gameObject);
			return;
		}

		if (ReachedTarget()) {
			OnTargetReached(_damage, _target);
			_target = null;
			_followTarget = false;
			Destroy(gameObject);
			return;
		}
		
		Quaternion targetRotation = Quaternion.LookRotation(TargetPosition - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed);
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * _speed);
	}
}
