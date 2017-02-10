using System.Collections.Generic;

public class Modifier {
	private Stats _target;
	private string _tag;
	private object _source;
	private readonly List<KeyValuePair<string, StatModifier>> _modifiers;

	public Stats target { get { return _target; } }
	public string tag { get { return _tag; } }
	public object source { get { return _source; } }
	public KeyValuePair<string, StatModifier>[] modifiers { get { return _modifiers.ToArray(); } }

	public Modifier(Stats target, string tag, object source = null) {
		_target = target;
		_tag = tag;
		_source = source;
		_modifiers = new List<KeyValuePair<string, StatModifier>>();
	}

	public void AddStatModifier(string stat, StatModifier modifier) {
		_modifiers.Add(new KeyValuePair<string, StatModifier>(stat, modifier));
	}
}
