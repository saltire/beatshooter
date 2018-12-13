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

	// AudioSource music;

	void Awake() {
		barLength = 60 / bpm * beatsPerBar;

		// music = GetComponent<AudioSource>();
	}

	void Update() {
		float time = Time.time - (Time.time > 0 ? startOffset : 0);
		// float time = music.time;
		int bar = (int)(time / barLength);
		float barTime = time % barLength;

		if (OnWhole != null && barTime <= Time.deltaTime) {
			OnWhole(bar, 0);
		}
		if (OnHalf != null && barTime % (barLength / 2) <= Time.deltaTime) {
			OnHalf(bar, (int)(barTime * 2 / barLength));
		}
		if (OnQuarter != null && barTime % (barLength / 4) <= Time.deltaTime) {
			OnQuarter(bar, (int)(barTime * 4 / barLength));
		}
		if (OnEighth != null && barTime % (barLength / 8) <= Time.deltaTime) {
			OnEighth(bar, (int)(barTime * 8 / barLength));
		}
		if (OnSixteenth != null && barTime % (barLength / 16) <= Time.deltaTime) {
			OnSixteenth(bar, (int)(barTime * 16 / barLength));
		}
	}
}
