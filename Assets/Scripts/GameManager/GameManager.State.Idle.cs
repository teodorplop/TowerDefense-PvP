using UnityEngine;

public partial class GameManager {
	private ActionHandler[] Idle_ActionHandlers = new ActionHandler[0];

	private void Idle_HandleMouseDown(int mouse, Vector3 position) {
		Tower tower = InputScanner.ScanFor<Tower>(position, _towerMask);
		_uiManager.ShowUpgrades(tower);
	}
}
