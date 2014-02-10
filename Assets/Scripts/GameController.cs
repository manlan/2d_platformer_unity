using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public KeyCode pause;
	private bool paused;
	public GUIText pausedText;
	
	void Start() {

		this.paused = false;
		this.pausedText.enabled = false;
	}
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(this.pause)) {
			if(!this.paused) {
				Time.timeScale = 0;
				this.paused = true;
				this.pausedText.enabled = true;
			}
			else {
				Time.timeScale = 1;
				this.paused = false;
				this.pausedText.enabled = false;
			}
		}
//		Debug.Log(Time.timeScale);
	}
}
