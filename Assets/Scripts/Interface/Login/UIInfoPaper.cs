using UnityEngine;
using DG.Tweening;
using Interface;

public class UIInfoPaper : MonoBehaviour {
	[SerializeField] private AnimatedLabel label;
	[SerializeField] private float delay = 3.0f;
	
	private DOTweenAnimation tween;
	private float timer;
	private bool hidden = true;

	void Awake() {
		tween = GetComponent<DOTweenAnimation>();

		EventManager.AddListener<ServerRequestEvent>(OnServerRequest);
		EventManager.AddListener<ServerResponseEvent>(OnServerResponse);
	}
	void OnDestroy() {
		EventManager.RemoveListener<ServerRequestEvent>(OnServerRequest);
		EventManager.RemoveListener<ServerResponseEvent>(OnServerResponse);
	}

	private void OnServerRequest(ServerRequestEvent e) {
		label.text = e.Element;
		label.enabled = true;
		tween.DOPlayForward();
		hidden = false;
	}
	private void OnServerResponse(ServerResponseEvent e) {
		label.text = e.Element;
		label.enabled = false;
		tween.DOPlayForward();
		hidden = false;
	}

	void Update() {
		if (hidden) return;

		timer += Time.deltaTime;
		if (timer >= delay) {
			tween.DOPlayBackwards();
			hidden = true;
			timer = 0.0f;
		}
	}
}
