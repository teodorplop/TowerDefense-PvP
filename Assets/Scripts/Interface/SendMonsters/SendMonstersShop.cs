﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Interface.sendMonsters {
	public class SendMonstersShop : MonoBehaviour {
		[SerializeField] private SendMonsterElement sendMonsterPrefab;
		
		private SendMonstersList sendMonsters;
		private List<SendMonsterElement> elements;

		void Awake() {
			elements = new List<SendMonsterElement>();
		}

		public void Inject(SendMonstersList sendMonsters) {
			this.sendMonsters = sendMonsters;
		}

		private void OnFirstShow() {
			SendMonsterElement elm;
			foreach (MonsterToSend monster in sendMonsters.Monsters) {
				elm = Instantiate(sendMonsterPrefab);
				elm.transform.SetParent(transform);
				elm.transform.localScale = Vector3.one;
				elm.gameObject.SetActive(true);
				elm.Inject(monster);

				elements.Add(elm);
			}

			sendMonsters = null;
		}

		public void Show(bool shown) {
			if (shown && sendMonsters != null)
				OnFirstShow();
			else {
				foreach (SendMonsterElement elm in elements)
					elm.gameObject.SetActive(shown);
			}
		}
	}
}