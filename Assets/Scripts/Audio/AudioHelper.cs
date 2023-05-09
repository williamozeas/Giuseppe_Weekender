using System.Collections;
using UnityEngine;

public static class AudioHelper {

	public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float volume = 1) {
			audioSource.Play();
			audioSource.volume = 0f;
			while (audioSource.volume < volume) {
				audioSource.volume += Time.deltaTime / FadeTime;
				yield return null;
		}
	}
	
}