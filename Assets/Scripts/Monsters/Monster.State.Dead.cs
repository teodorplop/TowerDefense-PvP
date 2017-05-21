using System.Collections;

public partial class Monster {
	private bool _isDead;
	public bool IsDead { get { return _isDead; } }

	IEnumerator Dead_EnterState() {
		_isDead = true;
		yield return null;
	}
}
