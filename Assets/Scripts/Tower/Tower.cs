using MoonSharp.Interpreter;

[MoonSharpUserData]
public class Tower {
	private readonly TowerBase _towerBase;
	public TowerBase towerBase { get { return _towerBase; } }

	private readonly Stats _stats;
	public Stats stats { get { return _stats; } }

	public Tower(TowerBase towerBase) {
		_towerBase = towerBase;
		_stats = new Stats(_towerBase.baseStats);

		int x = System.Convert.ToInt32(towerBase.actions.Call("OnSpawn", this));
		GDebug.Log(x);
	}
}
