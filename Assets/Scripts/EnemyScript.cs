using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	public Sprite enemySprite;

	public float bigGrow = 1.6f;
	public float smallGrow = 1.25f;
	public float shrinkDuration = .25f;
	float shrinkVelocity = 0;

	public float shakeAmount = .15f;
	public float moveDuration = .1f;
	Vector3 moveVelocity = Vector3.zero;
	
	public bool moveOnKick = false;
	public float moveAngle = 15;

	public GameObject bulletPrefab;
	public int bulletCount = 8;
	public float bulletSpeed = 2;
	public float bulletAngleOffset = .5f;

	bool hatched = false;

	Vector3 targetPosition;
	Vector3 shakyTargetPosition;

	SpriteRenderer spriter;

	void Awake() {
		spriter = GetComponent<SpriteRenderer>();
	}

	void Start() {
		targetPosition = transform.position;

		MidiParser.OnKick += OnKick;
		MidiParser.OnSnare += OnSnare;

		Grow(false);
	}

	void Update() {
		if (transform.localScale.x > 1) {
			float scale = Mathf.SmoothDamp(transform.localScale.x, 1, ref shrinkVelocity, shrinkDuration);
			transform.localScale = new Vector3(scale, scale, 1);
		}

		shakyTargetPosition = targetPosition + (Vector3)Random.insideUnitCircle * shakeAmount;
		transform.position = Vector3.SmoothDamp(transform.position, shakyTargetPosition,
			ref moveVelocity, moveDuration);
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
		transform.localScale = new Vector3(scale, scale, 1);

		if (!hatched && big) {
			GetComponent<SpriteRenderer>().sprite = enemySprite;
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

			bullet.GetComponent<SpriteRenderer>().color = spriter.color;
		}
	}

	public void SetColor(Color color) {
		spriter.color = color;
	}
}
