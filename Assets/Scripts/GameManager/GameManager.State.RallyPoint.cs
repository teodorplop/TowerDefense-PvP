using UnityEngine;
using Ingame.towers;

public partial class GameManager {
	private Tower _rallyPointTower;

	private void RallyPoint_HandleMouseDown(int mouse, Vector3 position) {
		Vector3 worldPosition;
		if (InputScanner.PlanePosition(position, out worldPosition)) {
			(_rallyPointTower as BarracksTower).ResetRallyPoint(worldPosition);
		}

		DeselectTower();
		SetState(GameState.Idle);
	}
}
