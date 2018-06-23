using UnityEngine;
using Interface;

public class PlayMenu : MonoBehaviour {
	[SerializeField] private GameObject findMatchIdle;
	[SerializeField] private GameObject findMatchInProgress;
	[SerializeField] private TimerLabel findMatchTimer;
	[SerializeField] private ModSelectionMenu modSelection;

	void Awake() {
		EventManager.AddListener<FindMatchStartedEvent>(OnFindMatchStarted);
		EventManager.AddListener<FindMatchCanceledEvent>(OnFindMatchCanceled);
	}
	void OnDestroy() {
		EventManager.RemoveListener<FindMatchStartedEvent>(OnFindMatchStarted);
		EventManager.RemoveListener<FindMatchCanceledEvent>(OnFindMatchCanceled);
	}

	void Start() {
		findMatchIdle.SetActive(true);
		findMatchInProgress.SetActive(false);
	}

	private void OnFindMatchStarted(FindMatchStartedEvent evt) {
		findMatchIdle.SetActive(false);
		findMatchInProgress.SetActive(true);

		findMatchTimer.Play();
	}
	private void OnFindMatchCanceled(FindMatchCanceledEvent evt) {
		findMatchIdle.SetActive(true);
		findMatchInProgress.SetActive(false);

		findMatchTimer.Stop();
	}

	public void FindMatch() {
		App.FindMatch(modSelection.SelectedMod);
	}

	public void CancelFindMatch() {
		App.CancelFindMatch();
	}
}
