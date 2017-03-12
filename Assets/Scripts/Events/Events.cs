public class TowerConstructionEvent : GameEvent {
	private TowerBase _target;
	public TowerBase target { get { return _target; } }
	public TowerConstructionEvent(TowerBase target) {
		_target = target;
	}
}