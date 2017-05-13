using UnityEngine;
using System.Collections.Generic;
using Utils.Linq;

public class Stats {
	private Dictionary<string, float> _baseStats;

	private Dictionary<string, float> _stats;
	private Dictionary<string, List<StatModifier>> _statModifiers;
	private List<Modifier> _modifiers;

	public Stats(Dictionary<string, float> baseStats) {
		_baseStats = new Dictionary<string, float>(baseStats);
		_stats = new Dictionary<string, float>(baseStats);
		_statModifiers = new Dictionary<string, List<StatModifier>>();
		_modifiers = new List<Modifier>();
	}

	/// <summary>
	/// Returns float value for a certain stat.
	/// </summary>
	public float GetStat(string stat) {
		if (!_baseStats.ContainsKey(stat)) {
			StatNotFound(stat);
			return 0.0f;
		}

		return _stats[stat];
	}

	/// <summary>
	/// Sets the value of a stat.
	/// </summary>
	public void SetStat(string stat, float value) {
		if (!_baseStats.ContainsKey(stat)) {
			StatNotFound(stat);
		} else {
			_stats[stat] = value;
		}
	}

	/// <summary>
	/// Applies modifier and recomputes stats.
	/// </summary>
	public void AddModifier(Modifier modifier) {
		_modifiers.Add(modifier);
		foreach (KeyValuePair<string, StatModifier> statModifier in modifier.modifiers) {
			_statModifiers[statModifier.Key].Add(statModifier.Value);
		}

		RecomputeStats();
	}

	/// <summary>
	/// Removes modifier and recomputes stats.
	/// </summary>
	public void RemoveModifier(Modifier modifier) {
		foreach (KeyValuePair<string, StatModifier> statModifier in modifier.modifiers) {
			_statModifiers[statModifier.Key].Remove(statModifier.Value);
		}
		_modifiers.Remove(modifier);

		RecomputeStats();
	}

	/// <summary>
	/// Removes all modifiers with a certain tag.
	/// </summary>
	public void RemoveModifiersByTag(string tag) {
		IEnumerable<Modifier> toRemove = _modifiers.Where(obj => obj.tag == tag);
		foreach (Modifier modifier in toRemove) {
			RemoveModifier(modifier);
		}
	}

	/// <summary>
	/// Removes all modifiers coming from a certain source.
	/// </summary>
	public void RemoveModifiersFromSource(object source) {
		IEnumerable<Modifier> toRemove = _modifiers.Where(obj => obj.source == source);
		foreach (Modifier modifier in toRemove) {
			RemoveModifier(modifier);
		}
	}

	/// <summary>
	/// Recomputes values for all stats.
	/// </summary>
	private void RecomputeStats() {
		foreach (KeyValuePair<string, float> stat in _baseStats) {
			_stats[stat.Key] = RecomputeStat(stat.Key);
		}
	}

	/// <summary>
	/// Recomputes and returns value for a certain stat.
	/// </summary>
	private float RecomputeStat(string stat) {
		if (!_baseStats.ContainsKey(stat)) {
			StatNotFound(stat);
			return 0.0f;
		}

		float value = _baseStats[stat];
		foreach (StatModifier statModifier in _statModifiers[stat]) {
			value = statModifier.Apply(value);
		}
		return value;
	}

	/// <summary>
	/// What to do in case of error.
	/// </summary>
	private static void StatNotFound(string stat) {
		Debug.LogError("Stat " + stat + " not found.");
	}
}
