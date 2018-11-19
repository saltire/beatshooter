using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumsScript : MonoBehaviour {
	public Blinker kick;
	public Blinker snare;

	int[] kicks = new int[] {
		1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0,
		1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0,
	};

	void Start() {
		MusicTimer.OnSixteenth += OnSixteenth;
	}

	void OnSixteenth(int bar, int beat) {
		int kbeat = ((bar % 2) << 4) + beat;
		if (kicks[kbeat] == 1) {
			kick.Blink();
		}

		if ((bar % 2 == 0 && beat == 8) || (bar % 2 == 1 && beat == 12) ||
			(bar % 16 == 7 && (beat == 13 || beat == 15))) {
			snare.Blink();
		}
	}
}
