using UnityEngine;

public partial class GameManager {
#pragma warning disable 0414
	private ActionHandler[] Idle_ActionHandlers = new ActionHandler[0];
#pragma warning restore 0414

	private void Idle_HandleMouseDown(int mouse, Vector3 position) {
		Tower tower = InputScanner.ScanFor<Tower>(position, _towerMask);
		if (tower != null && tower.owner != Players.ClientPlayer) {
			// TODO: Show something else here.
			_uiManager.ShowUpgrades(null);
		} else {
			_uiManager.ShowUpgrades(tower);
		}
	} 
}
