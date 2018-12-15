using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public Sprite enemySprite;

	public float bigGrow = 1.6f;
	public float smallGrow = 1.25f;
	public float shrinkLength = .25f;
	float shrinkVelocity = 0;

	public float shakeAmount = .15f;
	public float shakeLength = .1f;
	Vector3 shakeVelocity = Vector3.zero;

	bool hatched = false;

	Transform sprite;

	Vector3 targetPosition;
	Vector3 shakyTargetPosition;

	void Start() {
		sprite = transform.GetChild(0);

		targetPosition = transform.position;

		MidiParser.OnKick += () => Grow(false);
		MidiParser.OnSnare += () => Grow(true);

		Grow(false);
	}

	void Grow(bool big) {
		float scale = big ? bigGrow : smallGrow;
		sprite.localScale = new Vector3(scale, scale, 1);

		if (!hatched && big) {
			sprite.GetComponent<SpriteRenderer>().sprite = enemySprite;
			hatched = true;
		}
	}

	void Update() {
		if (sprite.localScale.x > 1) {
			float scale = Mathf.SmoothDamp(sprite.localScale.x, 1, ref shrinkVelocity, shrinkLength);
			sprite.localScale = new Vector3(scale, scale, 1);
		}

		shakyTargetPosition = targetPosition + (Vector3)Random.insideUnitCircle * shakeAmount;
		transform.position = Vector3.SmoothDamp(transform.position, shakyTargetPosition,
			ref shakeVelocity, shakeLength);
	}
}
