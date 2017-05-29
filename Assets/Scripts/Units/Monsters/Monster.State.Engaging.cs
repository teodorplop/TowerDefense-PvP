public partial class Monster {
	protected override void OnEngageTargetLost() {
		base.OnEngageTargetLost();
		SetState(BaseUnitState.Walking);
	}
}
