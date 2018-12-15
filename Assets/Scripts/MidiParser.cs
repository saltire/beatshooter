using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NAudio.Midi;

public class MidiParser : MonoBehaviour {
	public delegate void DrumEvent();
	public static event DrumEvent OnKick;
	public static event DrumEvent OnSnare;

	public delegate void TickEvent(int num);
	public static event TickEvent OnTick;
	public static event TickEvent OnBar;

	public string filename;
	public float bpm = 110;

	AudioSource music;
	MidiFile mf;
	List<NoteOnEvent> events = new List<NoteOnEvent>();

	int beatsInBar = 4;
	int ticksInBar;
	float latestTicks = -1;

	void Start() {
		music = GetComponent<AudioSource>();
		mf = new MidiFile(string.Format("Assets/Music/{0}.mid", filename));

		for (int t = 0; t < mf.Tracks; t++) {
			foreach (MidiEvent ev in mf.Events[t]) {
				if (ev.CommandCode == MidiCommandCode.MetaEvent &&
					((MetaEvent)ev).MetaEventType == MetaEventType.TimeSignature) {
					beatsInBar = ((TimeSignatureEvent)ev).Numerator;
					ticksInBar = beatsInBar * mf.DeltaTicksPerQuarterNote;
				}
				else if (MidiEvent.IsNoteOn(ev)) {
					events.Add((NoteOnEvent)ev);
				}
			}
		}
	}

	public float GetTicks() {
		return music.time / 60 * bpm * mf.DeltaTicksPerQuarterNote;
	}

	public float GetBar() {
		return music.time / 60 * bpm / beatsInBar;
	}

	void Update() {
		float currentTicks = GetTicks();

		int currentTick = (int)Mathf.Floor(currentTicks);
		if (OnTick != null && currentTick > Mathf.Floor(latestTicks)) {
			OnTick(currentTick);
		}
		int currentBar = (int)Mathf.Floor(currentTicks / ticksInBar);
		if (OnBar != null && currentBar > Mathf.Floor(latestTicks / ticksInBar)) {
			OnBar(currentBar);
		}

		foreach (NoteOnEvent ev in events.Where(ev => ev.AbsoluteTime > latestTicks && ev.AbsoluteTime <= currentTicks)) {
			if (OnKick != null && ev.NoteName == "C5") {
				OnKick();
			}
			else if (OnSnare != null && ev.NoteName == "D5") {
				OnSnare();
			}
		}

		latestTicks = currentTicks;
	}
}
