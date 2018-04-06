using UnityEngine;
using System;

public class UserProfile {
	[SerializeField] private string displayName;
	[SerializeField] private int skill;

	public string DisplayName { get { return displayName; } }
	public int Skill { get { return skill; } }
}
