using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rofl : MonoBehaviour {
	public KeyCode key;
	public float stop;

	void Update () {
		if (Input.GetKeyDown(key)) {
			GetComponent<Animator>().SetBool("IsFighting", true);
			Invoke("Stop", stop);
		}
	}
	void Stop() {
		GetComponent<Animator>().SetBool("IsFighting", false);
	}
}
