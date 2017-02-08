using System;

[Serializable]
public abstract class StatModifier {
	public abstract float Apply(float target);
}
