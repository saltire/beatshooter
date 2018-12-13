using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public Sprite enemySprite;

	public float bigGrow = 1.6f;
	public float smallGrow = 1.25f;
	public float shrinkLength = .25f;
	float shrinkVelocity = 0;

	bool hatched = false;

	Transform sprite;

	void Start() {
		sprite = transform.GetChild(0);

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
	}
}
