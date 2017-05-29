using System.Collections;
using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public partial class BaseUnit {
	protected bool _isFighting;
	public bool IsFighting { get { return _isFighting; } }
	private List<BaseUnit> _targets;
	public BaseUnit Target { get { return _targets.Count == 0 ? null : _targets[0]; } }

	public void Engage(BaseUnit target) {
		bool engaging = Target == null;
		_targets.Add(target);

		if (engaging) {
			SetState(BaseUnitState.Engaging);
		}
	}

	public void Disengage(BaseUnit target) {
		_targets.Remove(target);
	}

	public void RemoveTarget() {
		if (_targets.Count > 0) {
			if (_targets[0] != null) {
				_targets[0].Disengage(this);
			}
			_targets.RemoveAt(0);
		}
	}

	protected IEnumerator Engaging_EnterState() {
		if (Target == null) {
			OnEngageTargetLost();
			yield break;
		}

		Vector3 start = transform.position - owner.WorldOffset;
		Vector3 dest = Target.transform.position - owner.WorldOffset;
		PathRequestManager.RequestPath(owner, start, dest, EngagingPathReceived);

		yield return null;
	}

	private void EngagingPathReceived(bool success, Vector3[] waypoints) {
		if (!success) {
			Debug.LogError(name + " couldn't find path!", gameObject);
			return;
		}

		_waypointIndex = 0;
		_waypointsPath = new Path(waypoints, transform.position - owner.WorldOffset, 5.0f);
	}

	protected void Engaging_FixedUpdate() {
		if (Target != null && Target.CanBeAttacked()) {
			float distance = Vector3.Distance(transform.position, Target.transform.position);
			if (distance <= AttackRange) {
				EngagingPathFinished(true);
			} else {
				FollowWaypoints(EngagingPathFinished);
			}
		} else {
			OnEngageTargetLost();
		}
	}

	protected virtual void OnEngageTargetLost() {
		RemoveTarget();
	}

	private void EngagingPathFinished(bool now) {
		SetState(BaseUnitState.Fighting);
	}
}
