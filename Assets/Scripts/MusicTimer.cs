using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimer : MonoBehaviour {
	public delegate void BeatEvent(int bar, int beat);
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

	void Update() {
		float time = Time.time + startOffset;
		int bar = (int)(time / barLength);
		float beatTime = time % barLength;

		if (beatTime < Time.deltaTime) {
			OnWhole(bar, 0);
		}
		if (beatTime % (barLength / 2) <= Time.deltaTime) {
			OnHalf(bar, (int)(beatTime * 2 / barLength));
		}
		if (beatTime % (barLength / 4) <= Time.deltaTime) {
			OnQuarter(bar, (int)(beatTime * 4 / barLength));
		}
		if (beatTime % (barLength / 8) <= Time.deltaTime) {
			OnEighth(bar, (int)(beatTime * 8 / barLength));
		}
		if (beatTime % (barLength / 16) <= Time.deltaTime) {
			OnSixteenth(bar, (int)(beatTime * 16 / barLength));
		}
	}
}
