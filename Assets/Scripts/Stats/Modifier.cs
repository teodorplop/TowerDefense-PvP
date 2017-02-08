using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Modifier {
	[SerializeField]
	private List<KeyValuePair<string, StatModifier>> _statModifiers;
	public List<KeyValuePair<string, StatModifier>> statModifiers { get { return _statModifiers; } }

	public Modifier() {
		_statModifiers = new List<KeyValuePair<string, StatModifier>>();
	}

	public void AddModifier(string stat, StatModifier modifier) {
		_statModifiers.Add(new KeyValuePair<string, StatModifier>(stat, modifier));
	}
	public void Apply(Stats stats) {
		stats.AddModifier(this);
	}
	public void Remove(Stats stats) {
		stats.RemoveModifier(this);
	}
}
