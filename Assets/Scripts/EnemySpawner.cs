using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public EnemyScript enemyPrefab;
	public float circleRadius = 4;
	public float moveAngle = 15;
	public float spreadAngle = 45;
	public float startingBar = 0;
	public Color enemyColor = Color.white;

	List<EnemyScript> enemies = new List<EnemyScript>();

	void Start() {
		MidiParser.OnBar += OnBar;
	}

	void OnBar(int bar) {
		if (bar == startingBar) {
			SpawnEnemyOnCircle(0);
		}
		else if (bar == startingBar + 2) {
			SpawnEnemyOnCircle(spreadAngle);
			SpawnEnemyOnCircle(-spreadAngle);
		}

		if (bar == startingBar + 4) {
			foreach (EnemyScript enemy in enemies) {
				enemy.moveOnKick = true;
			}
		}
	}

	void SpawnEnemyOnCircle(float angle) {
		Vector3 position = Quaternion.Euler(0, 0, angle) * Vector3.up * circleRadius;
		EnemyScript enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
		enemy.SetColor(enemyColor);
		enemy.moveAngle = moveAngle;
		enemies.Add(enemy);
	}
}
