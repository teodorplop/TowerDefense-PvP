using UnityEngine;

namespace Ingame.towers {
	public class ProjectileTower : OffensiveTower {
		[SerializeField]
		protected Transform _launcher;
		[SerializeField]
		protected Transform _launchPoint;
		[SerializeField]
		protected Projectile _projectilePrefab;
		[SerializeField]
		protected GameObject _shootEffect;
		[SerializeField]
		protected ProjectileArc _projectileArcPrefab;

		private ProjectileArc _projectileArc;
		protected Quaternion _launcherRotation;
		protected float _currentSpeed;

		protected override void Start() {
			base.Start();
			_launcherRotation = _launcher.rotation;

			if (_debug) {
				_projectileArc = Instantiate(_projectileArcPrefab);
				_projectileArc.transform.SetParent(_launchPoint);
				_projectileArc.transform.localPosition = Vector3.zero;
				_projectileArc.transform.localRotation = Quaternion.identity;
			}
		}

		protected override void OnAttack(Monster target) {
			base.OnAttack(target);

			RotateTowards(_target == null ? null : _target.transform);
			LaunchProjectile();
		}

		protected override void Active_Update() {
			RotateTowards(_target == null ? null : _target.transform);
			base.Active_Update();
		}

		protected virtual void RotateTowards(Transform target) {
			Quaternion targetRotation = _launcherRotation;

			if (target != null) {
				Vector3 p1 = target.position;
				p1.y = 0;
				Vector3 p2 = transform.position;
				p2.y = 0;

				targetRotation = Quaternion.LookRotation(p1 - p2);
			}

			_launcher.rotation = targetRotation;
		}

		private void SetTargetWithAngle(Vector3 point, float angle) {
			Vector3 direction = point - _launchPoint.position;
			float yOffset = -direction.y;
			direction = Math3d.ProjectVectorOnPlane(Vector3.up, direction);
			float distance = direction.magnitude;

			_currentSpeed = ProjectileMath.LaunchSpeed(distance, yOffset, Physics.gravity.magnitude, angle * Mathf.Deg2Rad);

			if (_projectileArc)
				_projectileArc.UpdateArc(_currentSpeed, distance, Physics.gravity.magnitude, angle * Mathf.Deg2Rad, direction, true);
		}

		protected virtual void LaunchProjectile() {
			SetTargetWithAngle(_target.transform.position, -_launchPoint.transform.rotation.eulerAngles.x);

			Projectile projectile = Instantiate(_projectilePrefab);
			projectile.transform.SetParent(_launchPoint);
			projectile.transform.localPosition = Vector3.zero;
			projectile.transform.localRotation = Quaternion.identity;
			Rigidbody rg = projectile.GetComponent<Rigidbody>();
			if (rg != null) rg.velocity = _launchPoint.forward * _currentSpeed;
			projectile.Inject(Attack, AttackDamage, _target, owner);
			projectile.gameObject.SetActive(true);

			if (_shootEffect) {
				GameObject shootEffect = Instantiate(_shootEffect);
				shootEffect.transform.SetParent(_launchPoint);
				shootEffect.transform.localPosition = Vector3.zero;
				shootEffect.transform.localRotation = Quaternion.identity;
				shootEffect.SetActive(true);
			}
		}
	}
}