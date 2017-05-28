using System;
using UnityEngine;

namespace Ingame.towers {
	public class Tower : StateMachineBase {
		protected static readonly float _constructionTime = 1.0f;

		[SerializeField]
		protected TowerAttributes _attributes;
		[SerializeField]
		protected bool _debug;

		public float SellValue { get { return _attributes.sellValue; } }
		public string[] Upgrades { get { return _attributes.upgrades; } }

		public Player owner;

		protected void SetState(Enum state) {
			if (currentState == null || currentState.GetType() != state.GetType() || currentState.CompareTo(state) != 0) {
				_stateMachineHandler.SetState(state, this);
			}
		}

		public void SetAttributes(TowerAttributes attributes) {
			_attributes = attributes;
		}
	}
}
