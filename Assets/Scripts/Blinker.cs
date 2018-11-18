using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour {
	public Color color = Color.white;
	public float fadeTime = .0625f;

	SpriteRenderer spriter;
	float lastBlink;

	void Awake() {
		spriter = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (spriter.color != Color.black) {
			spriter.color = Color.Lerp(color, Color.black, (Time.time - lastBlink) / fadeTime);
		}
	}

	public void Blink() {
		spriter.color = color;
		lastBlink = Time.time;
	}
}
