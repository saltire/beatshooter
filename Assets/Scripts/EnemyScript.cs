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
	
	public bool moveOnKick = false;
	public float moveAngle = 15;

	public GameObject bulletPrefab;
	public int bulletCount = 8;
	public float bulletSpeed = 2;
	public float bulletAngleOffset = .5f;

	bool hatched = false;

	Transform sprite;

	Vector3 targetPosition;
	Vector3 shakyTargetPosition;

	void Start() {
		sprite = transform.GetChild(0);

		targetPosition = transform.position;

		MidiParser.OnKick += OnKick;
		MidiParser.OnSnare += OnSnare;

		Grow(false);
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

	void OnKick() {
		Grow(false);

		if (moveOnKick) {
			Move();
		}
	}

	void OnSnare() {
		Grow(true);

		Fire();
	}

	void Grow(bool big) {
		float scale = big ? bigGrow : smallGrow;
		sprite.localScale = new Vector3(scale, scale, 1);

		if (!hatched && big) {
			sprite.GetComponent<SpriteRenderer>().sprite = enemySprite;
			hatched = true;
		}
	}

	void Move() {
		targetPosition = Quaternion.Euler(0, 0, moveAngle) * targetPosition;
	}

	void Fire() {
		for (int i = 0; i < bulletCount; i++) {
			GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			bullet.GetComponent<BulletScript>().movement = 
				Quaternion.Euler(0, 0, (i + bulletAngleOffset) * 360 / bulletCount) * 
				Vector3.up * bulletSpeed;
		}
	}
}
