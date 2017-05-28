using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : BaseUnit {
	public enum UnitState { RallyPoint }

	public void SetRallyPoint(Vector3 point) {
		_rallyPoint = point;
		SetState(UnitState.RallyPoint);
	}
}
