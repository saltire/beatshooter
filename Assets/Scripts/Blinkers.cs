using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinkers : MonoBehaviour {
	public Blinker dotPrefab;

	Blinker whole;
	Blinker[] halves = new Blinker[2];
	Blinker[] quarters = new Blinker[4];
	Blinker[] eighths = new Blinker[8];
	Blinker[] sixteenths = new Blinker[16];

	void Start() {
		MusicTimer.OnWhole += OnWhole;
		MusicTimer.OnHalf += OnHalf;
		MusicTimer.OnQuarter += OnQuarter;
		MusicTimer.OnEighth += OnEighth;
		MusicTimer.OnSixteenth += OnSixteenth;

		MusicTimer timer = GameObject.FindObjectOfType<MusicTimer>();
		float barLength = timer.barLength;

		whole = Instantiate<Blinker>(dotPrefab, new Vector3(-8, 0, 0), Quaternion.identity);
		whole.fadeTime = barLength;

		for (int i = 0; i < 16; i++) {
			if (i % 8 == 0) {
				halves[i / 8] = Instantiate<Blinker>(dotPrefab, new Vector3(-8 + i, -1, 0), Quaternion.identity);
				halves[i / 8].fadeTime = barLength / 2;
			}
			if (i % 4 == 0) {
				quarters[i / 4] = Instantiate<Blinker>(dotPrefab, new Vector3(-8 + i, -2, 0), Quaternion.identity);
				quarters[i / 4].fadeTime = barLength / 4;
			}
			if (i % 2 == 0) {
				eighths[i / 2] = Instantiate<Blinker>(dotPrefab, new Vector3(-8 + i, -3, 0), Quaternion.identity);
				eighths[i / 2].fadeTime = barLength / 8;
			}
			sixteenths[i] = Instantiate<Blinker>(dotPrefab, new Vector3(-8 + i, -4, 0), Quaternion.identity);
			sixteenths[i].fadeTime = barLength / 16;
		}
	}

	void OnWhole(int beat) {
		whole.Blink();
	}

	void OnHalf(int beat) {
		halves[beat].Blink();
	}

	void OnQuarter(int beat) {
		quarters[beat].Blink();
	}

	void OnEighth(int beat) {
		eighths[beat].Blink();
	}

	void OnSixteenth(int beat) {
		sixteenths[beat].Blink();
	}
}
