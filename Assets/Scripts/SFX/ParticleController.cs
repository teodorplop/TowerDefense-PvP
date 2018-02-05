using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
	[SerializeField] bool waitForAudioSource = true;
	[SerializeField] bool emitOnAwake = true;

	private List<ParticleSystem> particles;

	void Awake() {
		particles = new List<ParticleSystem>();

		if (gameObject.GetComponent<ParticleSystem>() != null)
			particles.Add(gameObject.GetComponent<ParticleSystem>());

		foreach (Transform child in transform)
			if (child.GetComponent<ParticleSystem>() != null)
				particles.Add(child.GetComponent<ParticleSystem>());

		if (!emitOnAwake)
			foreach (var particle in particles) {
				var emission = particle.emission;
				emission.enabled = false;
			}
	}

	public void BeginEmission() {
		foreach (var particle in particles) {
			var emission = particle.emission;
			emission.enabled = true;
		}
	}

	public void Emit(bool emit) {
		foreach (var particle in particles) {
			var main = particle.main;
			main.loop = emit;
		}
	}

	void Update() {
		if (waitForAudioSource && GetComponent<AudioSource>() && GetComponent<AudioSource>().isPlaying)
			return;

		foreach (var particle in particles)
			if (particle.IsAlive())
				return;

		Destroy(gameObject);
	}
}
