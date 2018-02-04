using System.Collections;
using UnityEngine;

public partial class BaseUnit {
	protected virtual IEnumerator Dead_EnterState() {
		_animator.SetBool("IsDead", true);
		GetComponent<Collider>().enabled = false;
		yield return null;
	}
}
