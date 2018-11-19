using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {
	public GameObject enemyPrefab;
	public int spawnBar = 0;

	void Start() {
		MusicTimer.OnWhole += Spawn;
	}

	void Spawn(int bar, int beat) {
		if (bar == spawnBar) {
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);

			MusicTimer.OnWhole -= Spawn;
			Destroy(gameObject);
		}
	}
}
