using UnityEngine;
using System.Collections;

public class AudioHelper : MonoBehaviour 
{
	// over loads
	public static void CreatePlayAudioObject(AudioClip aClip) {
		CreatePlayAudioObject(aClip, 1.0f);
	}
	public static void CreatePlayAudioObject(AudioClip aClip, float vol)  {
		CreatePlayAudioObject(aClip, vol, "AudioPlayObject");
	}
	// creates a dynamic Audio object to position and play in the world
	public static void CreatePlayAudioObject(AudioClip aClip, float vol, string objName)
	{
		// instance a new gameobject
		GameObject apObject = new GameObject(objName);
		// position the object in the world
		apObject.transform.position = Vector3.zero;
		
		// add an AudioSource component
		apObject.AddComponent<AudioSource>();
		// return this script for use
		AudioSource apAudio = apObject.GetComponent<AudioSource>();
		// initialize some AudioSource fields
		apAudio.playOnAwake = false;
		apAudio.rolloffMode = AudioRolloffMode.Linear;
		
		apAudio.loop = false;		
		apAudio.clip = aClip;
		apAudio.volume = vol;
		
		// play the clip
		apAudio.Play();
		// destroy this object after clip length
		Destroy(apObject, aClip.length);
	}
	
	// fade our AudioSource object based on speed (> 0 fades volume up, < 0 fades volume out, == 0 assumes the sound is playing and just destroys it)
	public static IEnumerator FadeAudioObject(GameObject aObject, float fadeSpeed)
	{
		Animation apAnim = aObject.GetComponent<Animation>();
		AudioSource aSource = aObject.GetComponent<AudioSource>();


		// we are not a fade audio object
		if (apAnim == null) 
		{ 
			// we simply destroy the object and return
			if (fadeSpeed <= 0)
			{
				Destroy (aObject);
			}

			// we are a psitive playing sound, so just play it
			if (fadeSpeed > 0 && aSource != null)
			{
				aSource.Play ();
			}

			return true;
		}

		// animation clip is default to fade out (1 to 0), these will look reveresed but they are correct
		if (fadeSpeed < 0) 
		{ 
			apAnim[apAnim.clip.name].time = apAnim[apAnim.clip.name].length; 
		}
		else  
		{ 
			apAnim[apAnim.clip.name].time = 0; 
		}
		
		// set our speed
		apAnim[apAnim.clip.name].speed = fadeSpeed; 
		
		// play the audio
		if (aSource.isPlaying == false)
		{
			aSource.Play();
		}		
		
		// play the fade
		apAnim.Play();
	
		// yield the length of the clip
		if (fadeSpeed < 0)
		{
			while(apAnim.isPlaying) { yield return new WaitForEndOfFrame(); }
			
			Destroy (aObject);
		}	
	}
	
	// over loads
	public static GameObject CreateGetFadeAudioObject(AudioClip aClip) {
		return CreateGetFadeAudioObject(aClip, true);
	}	
	public static GameObject CreateGetFadeAudioObject(AudioClip aClip, bool dLoop) {
		return CreateGetFadeAudioObject(aClip, dLoop, null);
	}
	public static GameObject CreateGetFadeAudioObject(AudioClip aClip, bool dLoop, AnimationClip fadeClip) {
		return CreateGetFadeAudioObject(aClip, dLoop, fadeClip, "ReturnedAudioPlayObject");
	}
	
	// creates/returns a dynamic Audio object to position and plays in the world with loop
	public static GameObject CreateGetFadeAudioObject(AudioClip aClip, bool dLoop, AnimationClip fadeClip, string objName)
	{
		// instance a new gameobject
		GameObject apObject = new GameObject(objName);
		// position the object in the world
		apObject.transform.position = Vector3.zero;
		// add our DontDestroyOnLoad
		DontDestroyOnLoad(apObject);		
		// add an AudioSource component
		apObject.AddComponent<AudioSource>();
		// return this script for use
		AudioSource apAudio = apObject.GetComponent<AudioSource>();
		// initialize some AudioSource fields
		apAudio.playOnAwake = false;
		apAudio.rolloffMode = AudioRolloffMode.Linear;
		
		apAudio.loop = dLoop;		
		apAudio.clip = aClip;
		apAudio.volume = 1.0f; // default
		
		if (fadeClip != null)
		{
			Animation apAnim = apObject.AddComponent<Animation>();
			apAnim.AddClip(fadeClip, fadeClip.name);
			apAnim.clip = fadeClip;
			apAnim.playAutomatically = false;
		}
			
		
		// return our AudioObject
		return apObject;
	}
}
