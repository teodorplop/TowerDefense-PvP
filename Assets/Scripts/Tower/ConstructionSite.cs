using UnityEngine;

public class ConstructionSite : MonoBehaviour {
	[SerializeField]
	private string[] _towersAllowed;
	public string[] TowersAllowed { get { return _towersAllowed; } }
}
