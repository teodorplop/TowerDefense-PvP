using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Tower : ITickable {
	private readonly TowerBase _towerBase;
	public TowerBase towerBase { get { return _towerBase; } }

	private readonly Stats _stats;
	public Stats stats { get { return _stats; } }

	public Tower(TowerBase towerBase) {
		_towerBase = towerBase;
		_stats = new Stats(_towerBase.baseStats);
	}

	private void OnSpawn() {
		TickEngine.Register(this);
		towerBase.actions.Call("OnSpawn", this);
	}
	private void OnAttack() {
		towerBase.actions.Call("OnAttack", this);
	}
	private void OnSell() {
		towerBase.actions.Call("OnSell", this);
	}
	private void OnDestroy() {
		TickEngine.Unregister(this);
		towerBase.actions.Call("OnDestroy", this);
	}

	public void Tick() {
	}
}
