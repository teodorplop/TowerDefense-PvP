using DG.Tweening;

public class UITweenMenuPanel : UIMenuPanel {
	private DOTweenAnimation[] tweens;

	void OnEnable() {
		tweens = GetComponents<DOTweenAnimation>();
	}

	public override void Show() {
		shown = true;
		foreach (DOTweenAnimation tween in tweens)
			tween.DOPlayForward();
	}

	public override void Hide() {
		shown = false;
		foreach (DOTweenAnimation tween in tweens)
			tween.DOPlayBackwards();
	}
}
