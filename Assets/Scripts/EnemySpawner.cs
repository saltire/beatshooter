using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public int spawnBar = 0;

	void Start() {
		MidiParser.OnBar += OnBar;
	}

	void OnBar(int bar) {
		if (bar == spawnBar) {
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);

			MidiParser.OnBar -= OnBar;
			Destroy(gameObject);
		}
	}
}
