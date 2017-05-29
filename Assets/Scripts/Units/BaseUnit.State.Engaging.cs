using System.Collections;
using UnityEngine;
using Pathfinding;

public partial class BaseUnit {
	protected BaseUnit _target;
	protected Vector3 _engagePoint;

	public void Engage(BaseUnit target, Vector3 engagePoint) {
		_target = target;
		_engagePoint = engagePoint;
		SetState(BaseUnitState.Engaging);
	}

	protected IEnumerator Engaging_EnterState() {
		PathRequestManager.RequestPath(owner, transform.position - owner.WorldOffset, _engagePoint - owner.WorldOffset, EngagingPathReceived);
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
		if (_target != null && _target.CanBeAttacked()) {
			FollowWaypoints(EngagingPathFinished);
		} else {
			EngageTargetLost();
		}
	}

	protected virtual void EngageTargetLost() {
	}

	private void EngagingPathFinished(bool now) {
		SetState(BaseUnitState.Fighting);
	}
}
