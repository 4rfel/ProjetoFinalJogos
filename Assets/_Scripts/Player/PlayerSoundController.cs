using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class PlayerSoundController : NetworkBehaviour {

	AudioClip hit;
	AudioClip water;

	[SerializeField] AudioSource soundEffectAudioSource;
	public Slider soundEffectsAudioVolume;

	AudioClip music;
	[SerializeField] AudioSource BackgroundAudioSource;
	public Slider BackgroundAudioVolume;


	private void Start() {
		hit = Resources.Load<AudioClip>("hitting-ball");
		water = Resources.Load<AudioClip>("hitting-ball");
		music = Resources.Load<AudioClip>("water");
		BackgroundAudioSource.loop = true;
		BackgroundAudioSource.clip = music;
		BackgroundAudioSource.Play();

	}

	private void Update() {
		if (soundEffectsAudioVolume == null)
			soundEffectAudioSource.volume = 1f;
		else
			soundEffectAudioSource.volume = soundEffectsAudioVolume.value;

		if (IsLocalPlayer) {
			if (BackgroundAudioVolume == null)
				BackgroundAudioSource.volume = 1f;
			else
				BackgroundAudioSource.volume = BackgroundAudioVolume.value;

		} else { BackgroundAudioSource.volume = 0f; }
	}

	public void PlaySound(string clip) {
		switch (clip) {
			case "hit":
				soundEffectAudioSource.PlayOneShot(hit);
				break;
			case "water":
				soundEffectAudioSource.PlayOneShot(water);
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
