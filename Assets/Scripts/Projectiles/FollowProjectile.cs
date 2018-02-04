using UnityEngine;

public class FollowProjectile : Projectile {
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _turnSpeed;
	[SerializeField]
	private float _offset;
	[SerializeField]
	private float _gravity;

	private Vector3 _targetOffset;
	private Vector3 TargetPosition { get { return _target.transform.position + new Vector3(0, 0.5f); } }

	public override void Inject(int damage, Monster monster) {
		base.Inject(damage, monster);
		_targetOffset = new Vector3(0, _offset, 0);
		transform.LookAt(TargetPosition + _targetOffset);
	}

	private bool ReachedTarget() {
		return _target == null ? false : Vector3Utils.PlanarDistance(TargetPosition, transform.position) <= 0.75f;
	}
	private bool TargetIsStillValid() {
		return _target != null && _target.CanBeAttacked();
	}

	void FixedUpdate() {
		if (_target == null) {
			// We do not have a target yet
			return;
		}

		if (!TargetIsStillValid()) {
			// TODO: just throw this projectile somewhere on the ground
			Destroy(gameObject);
			return;
		}

		if (ReachedTarget()) {
			OnTargetImpact(_damage, _target);
			_target = null;
			Destroy(gameObject);
			return;
		}

		_targetOffset.y -= _gravity * Time.fixedDeltaTime;

		Quaternion targetRotation = Quaternion.LookRotation(TargetPosition + _targetOffset - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed);
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * _speed);

		if (transform.position.y < 0.0f)
			Destroy(gameObject);
	}
}
