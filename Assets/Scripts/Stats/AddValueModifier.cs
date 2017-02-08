using System;
using UnityEngine;

[Serializable]
public class AddValueModifier : StatModifier {
	[SerializeField]
	private float _add;
	public AddValueModifier(float add) {
		_add = add;
	}
	
	public override float Apply(float target) {
		return target + _add;
	}
}
