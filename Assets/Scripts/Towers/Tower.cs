using System;
using UnityEngine;

namespace Ingame.towers {
	public class Tower : StateMachineBase {
		protected static readonly float _constructionTime = 1.0f;

		[SerializeField]
		protected TowerAttributes _attributes;
		public string[] Upgrades { get { return _attributes.upgrades; } }

		public Player owner;

		protected void SetState(Enum state) {
			if (currentState == null || currentState != state) {
				_stateMachineHandler.SetState(state, this);
			}
		}

		public void SetAttributes(TowerAttributes attributes) {
			_attributes = attributes;
		}
	}
}
