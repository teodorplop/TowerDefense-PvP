using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Utils {
	public static float PlanarDistance(Vector3 a, Vector3 b) {
		a.y = 0;
		b.y = 0;
		return Vector3.Distance(a, b);
	}
}
