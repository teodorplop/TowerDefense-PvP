using System.Collections.Generic;
using System;
using UnityEngine;

namespace Ingame.waves {
	[Serializable]
	public class Wave {
		[SerializeField, Tooltip("Time from start of match until this wave spawn")]
		public float spawnTime;
		[SerializeField]
		public List<WaveMonster> monsters;

		[NonSerialized]
		private float _timer;
		[NonSerialized]
		public bool finished;

		public List<WaveMonster> Advance(float time) {
			_timer += time;

			finished = true;
			List<WaveMonster> monstersToSend = new List<WaveMonster>();
			foreach (WaveMonster monster in monsters) {
				if (!monster.sent) {
					finished = false;
					if (_timer >= monster.spawnTime) {
						monster.sent = true;
						monstersToSend.Add(monster);
					}
				}
			}

			return monstersToSend;
		}
	}

	[Serializable]
	public class WaveMonster {
		[SerializeField, Tooltip("Time from start of wave until these monsters spawn")]
		public float spawnTime;
		[SerializeField]
		public string name;
		[SerializeField]
		public string path;

		[NonSerialized]
		public bool sent;
	}

	public class WavesManager : MonoBehaviour {
		[SerializeField]
		private float _displacement = 2.5f;

		private List<Wave> _waves;
		private bool _started;
		private float _matchTimer;
		private MonsterFactory _monsterFactory;
		void Awake() {
			_waves = GameResources.LoadWaves();
			_monsterFactory = FindObjectOfType<MonsterFactory>();
		}

		public void StartMatch() {
			_started = true;
		}

		void Update() {
			if (!_started) {
				return;
			}

			_matchTimer += Time.deltaTime;

			foreach (Wave wave in _waves) {
				if (_matchTimer >= wave.spawnTime && !wave.finished) {
					SendMonsters(wave.Advance(Time.deltaTime));
				}
			}
		}

		private void SendMonsters(List<WaveMonster> monsters) {
			System.Random random = new System.Random(0);
			foreach (WaveMonster monster in monsters) {
				Vector2 offset = Vector2.zero;
				offset.x = ((float)random.NextDouble() * 2 - 1) * _displacement;
				offset.y = ((float)random.NextDouble() * 2 - 1) * _displacement;

				foreach (Player player in Players.GetPlayers()) {
					_monsterFactory.SendMonster(player, monster.name, monster.path, offset);
				}
			}
		}
	}
}
