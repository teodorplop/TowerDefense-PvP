using UnityEngine;

public class Tower : MonoBehaviour {
	[SerializeField]
	private string[] _upgrades;
	
	public string[] Upgrades { get { return _upgrades; } }

	public Player owner;
}
