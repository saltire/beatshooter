using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimer : MonoBehaviour {
	public delegate void BeatEvent(int beat);
	public static event BeatEvent OnWhole;
	public static event BeatEvent OnHalf;
	public static event BeatEvent OnQuarter;
	public static event BeatEvent OnEighth;
	public static event BeatEvent OnSixteenth;

	public float bpm;
	public int beatsPerBar = 4;
	public float barLength { get; private set; }

	public float startOffset = 0;

	void Awake() {
		barLength = 60 / bpm * beatsPerBar;
	}

	void FixedUpdate() {
		float beatTime = (Time.time + startOffset) % barLength;
		if (beatTime < Time.deltaTime) {
			OnWhole(0);
		}
		if (beatTime % (barLength / 2) <= Time.deltaTime) {
			OnHalf((int)(beatTime * 2 / barLength));
		}
		if (beatTime % (barLength / 4) <= Time.deltaTime) {
			OnQuarter((int)(beatTime * 4 / barLength));
		}
		if (beatTime % (barLength / 8) <= Time.deltaTime) {
			OnEighth((int)(beatTime * 8 / barLength));
		}
		if (beatTime % (barLength / 16) <= Time.deltaTime) {
			OnSixteenth((int)(beatTime * 16 / barLength));
		}
	}
}
