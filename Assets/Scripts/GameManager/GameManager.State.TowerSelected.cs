using UnityEngine;
using Ingame.towers;

public partial class GameManager {
#pragma warning disable 0414
	private ActionHandler[] TowerSelected_ActionHandlers = new ActionHandler[0];
#pragma warning restore 0414

	private void TowerSelected_HandleMouseDown(int mouse, Vector3 position) {
		Tower tower = InputScanner.ScanFor<Tower>(position, _towerMask);
		SelectTower(tower);

		if (_selectedTower == null) {
			BaseUnit unit = InputScanner.ScanFor<BaseUnit>(position, _unitMask);
			SelectUnit(unit);
		}
	}
}
