using System.Collections;
using UnityEngine;

public partial class Monster {
	private bool _reachedDestination;
	public bool ReachedDestination { get { return _reachedDestination; } }

	IEnumerator Destination_EnterState() {
		Debug.Log("Reached destination", gameObject);

		_reachedDestination = true;
		yield return null;
	}
}
