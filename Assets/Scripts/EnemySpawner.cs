using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float circleRadius = 4;

	List<GameObject> enemies = new List<GameObject>();

	void Start() {
		MidiParser.OnBar += OnBar;
	}

	void OnBar(int bar) {
		if (bar == 0) {
			SpawnEnemyOnCircle(0);
		}
		else if (bar == 2) {
			SpawnEnemyOnCircle(55);
			SpawnEnemyOnCircle(-55);
		}

		if (bar == 4) {
			foreach (GameObject enemy in enemies) {
				enemy.GetComponent<EnemyScript>().moveOnKick = true;
			}
		}
	}

	void SpawnEnemyOnCircle(float angle) {
		Vector3 position = Quaternion.Euler(0, 0, angle) * Vector3.up * circleRadius;
		enemies.Add(Instantiate(enemyPrefab, position, Quaternion.identity));
	}
}
