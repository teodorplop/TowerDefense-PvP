using UnityEngine;
using Ingame.towers;

public partial class GameManager {
#pragma warning disable 0414
	private ActionHandler[] Idle_ActionHandlers = new ActionHandler[0];
#pragma warning restore 0414

	private void Idle_HandleMouseDown(int mouse, Vector3 position) {
		Tower tower = InputScanner.ScanFor<Tower>(position, _towerMask);
		SelectTower(tower);

		if (_selectedTower == null) {
			BaseUnit unit = InputScanner.ScanFor<BaseUnit>(position, _unitMask);
			SelectUnit(unit);
		}
	}

	private Tower _selectedTower;
	private void DeselectTower() {
		if (_selectedTower != null) {
			_uiManager.ShowTower(null, null);
			_highlightManager.Disable(_selectedTower.gameObject);
			_selectedTower = null;
		}
	}
	private void SelectTower(Tower tower) {
		DeselectUnit();
		DeselectSendMonsters();
		_uiManager.ShowTower(null, null);

		if (_selectedTower == tower) {
			tower = null;
		}

		if (_selectedTower != null) {
			_highlightManager.Disable(_selectedTower.gameObject);
			_selectedTower.Select(false);
			_selectedTower = null;
		}

		if (tower == null) {
			SetState(GameState.Idle);
		} else if (tower.owner == Players.ClientPlayer) {
			SetState(GameState.TowerSelected);
			_highlightManager.Enable(tower.gameObject, true);
			_uiManager.ShowTower(_towerFactory, tower);
		} else {
			SetState(GameState.TowerSelected);
			_highlightManager.Enable(tower.gameObject, false);
			// TODO: Show something else here using UIManager.
		}

		_selectedTower = tower;
		if (_selectedTower != null) {
			_selectedTower.Select(true);
		}
	}

	private BaseUnit _selectedUnit;
	private void DeselectUnit() {
		if (_selectedUnit != null) {
			_uiManager.ShowUnit(null);
			_highlightManager.Disable(_selectedUnit.gameObject);
			_selectedUnit = null;
		}
	}
	private void SelectUnit(BaseUnit unit) {
		DeselectTower();
		DeselectSendMonsters();
		_uiManager.ShowUnit(null);

		if (_selectedUnit == unit) {
			unit = null;
		}

		if (_selectedUnit != null) {
			_highlightManager.Disable(_selectedUnit.gameObject);
			_selectedUnit = null;
		}

		if (unit == null) {
			SetState(GameState.Idle);
		} else {
			_highlightManager.Enable(unit.gameObject, (unit is Monster) ? false : true);
			_uiManager.ShowUnit(unit);
		}

		_selectedUnit = unit;
	}
	
	private bool _sendMonstersSelected;
	private void DeselectSendMonsters() {
		if (_sendMonstersSelected)
			_uiManager.ShowSendMonsters(_sendMonstersSelected = false);
	}
	private void SelectSendMonsters() {
		if (_sendMonstersSelected) {
			DeselectSendMonsters();
			return;
		}

		DeselectTower();
		DeselectUnit();
		_uiManager.ShowSendMonsters(_sendMonstersSelected = true);
	}
}
