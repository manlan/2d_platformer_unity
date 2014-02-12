using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour 
{
	// instance of class (Singleton)
	public static BaseManager instance = null;
	// audio clips to play looped and fade in/out 
//	public AudioClip introMusic;
//	public static GameObject globalIntroMusic;
//	public AudioClip menuMusic;
//	public static GameObject globalMenuMusic;
	public AudioClip gameMusic;
	public static GameObject globalGameMusic;
//	public AudioClip scoresMusic;
//	public static GameObject globalScoresMusic;
//	public AudioClip endMusic;
//	public static GameObject globalEndMusic;
	// our fade animation clip
	public AnimationClip fadeClip;
	// audio clips to play one time
	public AudioClip characterSwap;
//	public AudioClip sfxRespawn;
//	public AudioClip sfxBotExplo;
	// declare instance
	void OnEnable()
	{
		if (instance == null) 
		{
			instance = this;
			DontDestroyOnLoad (this);
		}
	}

}