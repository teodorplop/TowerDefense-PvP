using UnityEngine;
using System;
using System.Collections.Generic;
using Utils.Linq;

[Serializable]
public class PathDescription {
	[SerializeField]
	protected List<Vector2i> _points = new List<Vector2i>();
	/// <summary>
	/// Returns a read only collection for the points. Cache it when used.
	/// </summary>
	public List<Vector2i> Points { get { return _points.AsReadOnly().ToList(); } }
}
