using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Music : MonoBehaviour {

	public AudioClip[] music;
	public AudioClip charSwapExplosionNoise;

	void Start() {

		StartCoroutine(PlayRandom());
	}

	IEnumerator PlayRandom () {

		while(true) {
			this.audio.clip = music[getRandom()];
			this.audio.Play();
			yield return new WaitForSeconds(audio.clip.length);
		}
	}

	private int getRandom() {

		return Random.Range(0, this.music.Length);
	}
//
//	private void playCharSwapExplosion() {
//
//		this.charSwapExplosionNoise.
//	}
}
