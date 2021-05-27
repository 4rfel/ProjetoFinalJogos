using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class PlayerSoundController : NetworkBehaviour {


	AudioClip hit;
	[SerializeField] AudioSource soundEffectAudioSource;
	public Slider soundEffectsAudioVolume;

	[SerializeField] AudioSource BackgroundAudioSource;
	public Slider BackgroundAudioVolume;

	private void Start() {
		hit = Resources.Load<AudioClip>("hitting-ball");
	}

	private void Update() {
		if (IsLocalPlayer) {
			if (soundEffectsAudioVolume == null)
				soundEffectAudioSource.volume = 1f;
			else
				soundEffectAudioSource.volume = soundEffectsAudioVolume.value;

			if (BackgroundAudioVolume == null)
				BackgroundAudioSource.volume = 1f;
			else
				BackgroundAudioSource.volume = BackgroundAudioVolume.value;
		}
	}

	public void PlaySound(string clip) {
		switch (clip) {
			case "hit":
				soundEffectAudioSource.PlayOneShot(hit);
				break;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		//PlaySound("hit");
		switch (collision.gameObject.tag) {
			case "Player":
				PlaySound("hit");
				break;
		}
	}
}
