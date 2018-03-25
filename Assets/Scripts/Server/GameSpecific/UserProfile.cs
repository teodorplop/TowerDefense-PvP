using UnityEngine;
using System;

[Serializable]
public class UserProfile {
	[SerializeField] private string displayName;

	public string DisplayName { get { return displayName; } }
}
