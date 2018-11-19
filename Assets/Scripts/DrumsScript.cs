using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drums : MonoBehaviour {
	public Blinker kick;
	public Blinker snare;

	int[] kicks = new int[] {
		1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0,
		1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0,
	};

	void Start() {
		MusicTimer.OnSixteenth += Kick;
		MusicTimer.OnQuarter += Snare;
	}

	void Kick(int bar, int beat) {
		int bbeat = ((bar % 2) << 4) + beat;
		if (kicks[bbeat] == 1) {
			kick.Blink();
		}
	}

	void Snare(int bar, int beat) {
		if ((bar % 2 == 0 && beat == 2) || (bar % 2 == 1 && beat == 3)) {
			snare.Blink();
		}
	}
}
