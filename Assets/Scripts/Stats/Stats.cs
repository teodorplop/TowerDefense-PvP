using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Stats {
	[SerializeField]
	private Dictionary<string, float> _baseStats;

	private Dictionary<string, float> _stats;
	private Dictionary<string, List<StatModifier>> _statModifiers;

	public Stats() {
		_baseStats = new Dictionary<string, float>();
		_stats = new Dictionary<string, float>();
		_statModifiers = new Dictionary<string, List<StatModifier>>();
	}

	public float GetStat(string stat) {
		if (!_stats.ContainsKey(stat)) {
			Debug.LogError("Stat " + stat + " not found.");
			return 0.0f;
		}
		return _stats[stat];
	}

	public void AddModifier(Modifier modifier) {
		foreach (KeyValuePair<string, StatModifier> statModifier in modifier.statModifiers) {
			_statModifiers[statModifier.Key].Add(statModifier.Value);
		}

		RecomputeStats();
	}

	public void RemoveModifier(Modifier modifier) {
		foreach (KeyValuePair<string, StatModifier> statModifier in modifier.statModifiers) {
			_statModifiers[statModifier.Key].Remove(statModifier.Value);
		}

		RecomputeStats();
	}

	private void RecomputeStats() {
		foreach (KeyValuePair<string, float> stat in _baseStats) {
			float value = stat.Value;
			foreach (StatModifier statModifier in _statModifiers[stat.Key]) {
				value = statModifier.Apply(value);
			}
			_stats[stat.Key] = value;
		}
	}
}
