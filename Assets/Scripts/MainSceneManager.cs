using UnityEngine;
using System.Collections;

public class MainSceneManager : MonoBehaviour {
	
	void Start () {

		BaseManager.globalGameMusic = AudioHelper.CreateGetFadeAudioObject
			(BaseManager.instance.gameMusic, true, BaseManager.instance.fadeClip, "MainGameMusic");

		StartCoroutine(AudioHelper.FadeAudioObject(BaseManager.globalGameMusic, .5f));
	}
}
