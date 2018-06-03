using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public static EnemySpawner instance;
	public EnemySpawner.WaveData[] waveData;
	int curWaveIndex = 0;

	public GameObject[] enemyPrefabs;

	// current wave info
	float timeBetweenSpawns;
	float spawnTimer = 0;
	bool inWave = false;
	bool allEnemiesSpawned = false;
	float spawnX = 15f;
	float enemiesSpawned;
	float enemiesAlive;

	void Awake () {
		instance = this;
	}

	void Start () {
		WaveStart ();
	}
		
	void Update () {
		if (inWave && !allEnemiesSpawned) {
			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0f) {
				spawnTimer = timeBetweenSpawns;
				SpawnEnemy ();
			}
		}
	}

	void SpawnEnemy () {
		WaveData curWave = waveData [curWaveIndex];
		int enemyIndex = curWave.GetRandomEnemyIndex ();
		GameObject enemyPrefab = enemyPrefabs [enemyIndex];
		GameObject newEnemy = Instantiate (enemyPrefab, new Vector3 (spawnX, 0f, 0f), Quaternion.identity, transform);
		enemiesAlive++;
		enemiesSpawned++;

		if (enemiesSpawned >= curWave.enemyCount) {
			allEnemiesSpawned = true;
		}
	}

	public void WaveStart () {
		WaveData curWave = waveData [curWaveIndex];
		timeBetweenSpawns = curWave.duration / curWave.enemyCount;
		inWave = true;
		allEnemiesSpawned = false;
	}

	public void WaveComplete () {
		inWave = false;
		curWaveIndex++;
		if (curWaveIndex >= waveData.Length) {
			// no more waves
			print ("You win");
		} else {
			WaveStart ();
		}
	}

	public void EnemyDied () {
		enemiesAlive--;
		if (enemiesAlive <= 0f) {
			WaveComplete ();
		}
	}

	[System.Serializable]
	public class WaveData {
		public int[] enemyIndexes;
		public float enemyCount;
		public float duration;

		public int GetRandomEnemyIndex () {
			return enemyIndexes [Random.Range (0, enemyIndexes.Length)];
		}
	}
}
