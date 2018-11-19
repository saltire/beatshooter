using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public Sprite enemySprite;

	public float bigGrow = 1.6f;
	public float smallGrow = 1.25f;
	public float shrinkLength = .25f;
	float lastGrowTime;
	float shrinkVelocity = 0;

	bool hatched = false;

	int[] kicks = new int[] {
		1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0,
		1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0,
	};

	Transform sprite;

	void Start() {
		sprite = transform.GetChild(0);
		sprite.localScale = new Vector3(bigGrow, bigGrow, 1);

		MusicTimer.OnSixteenth += Grow;
	}

	void Grow(int bar, int beat) {
		int kbeat = ((bar % 2) << 4) + beat;
		if (kicks[kbeat] == 1 || (bar % 16 == 7 && (beat == 13 || beat == 15))) {
			float newScale = sprite.localScale.x * smallGrow;
			if (!hatched || kbeat == 8 || kbeat == 28 || (bar % 16 == 7 && (beat == 13 || beat == 15))) {
				newScale = bigGrow;
			}
			sprite.localScale = new Vector3(newScale, newScale, 1);
			lastGrowTime = Time.time;
		}

		if (!hatched && beat == 8) {
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
