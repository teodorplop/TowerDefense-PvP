public class UpgradeTowerRequest : ActionRequest {
	private string _tower;
	private string _upgrade;
	public UpgradeTowerRequest(string tower, string upgrade) {
		_tower = tower;
		_upgrade = upgrade;
	}

	public string Tower { get { return _tower; } }
	public string Upgrade { get { return _upgrade; } }

	public override string ToString() {
		return "UpgradeTowerRequest(" + _tower + ", " + _upgrade + ")";
	}
}
