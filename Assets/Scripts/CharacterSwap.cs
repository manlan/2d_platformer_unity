using UnityEngine;
using System.Collections;

public class CharacterSwap : MonoBehaviour {

	//the intermediary swap explosion
	public GameObject explosion;
	//the keypress as chosen via the inspector
	public KeyCode swapKey;
	//allow some grace before next swap
	public float timeBeforeNextSwap;
	//the actual character list array
	public GameObject[] characters;

	private GameObject currentCharacter;
	private int currentCharacterID;
	private Transform cam;
	private CameraFollow camFollow;


	void Awake() {

		//this script will destroy the character and instantiate the next one so we need to tell this new info to the main cam
		this.cam = Camera.main.transform;
		this.camFollow = cam.GetComponent<CameraFollow>();
		//we track current character via an ID
		this.currentCharacterID = 0;

		Vector3 characterPosition = new Vector3(this.cam.position.x, this.cam.position.y, 0);
		this.currentCharacter = Instantiate(this.characters[this.currentCharacterID], characterPosition, Quaternion.identity) as GameObject;
	}

	void Start () {

		this.StartCoroutine(CheckSwapChars());
	}

	IEnumerator CheckSwapChars() {

		float swapRate = 0;
		while (true) {
			if (Input.GetKeyDown(this.swapKey) && Time.time > swapRate) {
				StartCoroutine(SwapCharacters());
				swapRate = Time.time + this.timeBeforeNextSwap;
			}

			yield return null;
		}
	}

	IEnumerator SwapCharacters () {

		Vector3 currentCharPosition = this.currentCharacter.transform.position;
		this.currentCharacter.renderer.enabled = false;
		this.currentCharacterID++;

		this.camFollow.FollowPlayer();
		GameObject swapExplosion = Instantiate(this.explosion, currentCharPosition, Quaternion.identity) as GameObject;		
		AudioHelper.CreatePlayAudioObject(BaseManager.instance.characterSwap, .5f);
		yield return new WaitForSeconds(1.25f);
		Destroy(swapExplosion);

		GameObject newCurrentChar;
		switch (this.currentCharacterID) {
			case 0: newCurrentChar = Instantiate(this.characters[0], currentCharPosition, Quaternion.identity) as GameObject;
			break;
			case 1: newCurrentChar = Instantiate(this.characters[1], currentCharPosition, Quaternion.identity) as GameObject;
			break;
			case 2: newCurrentChar = Instantiate(this.characters[2], currentCharPosition, Quaternion.identity) as GameObject;
			break;
			default: this.currentCharacterID = 0;
			newCurrentChar = Instantiate(this.characters[0], currentCharPosition, Quaternion.identity) as GameObject;
			break;
		}

//		yield return StartCoroutine(this.camFollow.SetNewPlayer(newCurrentChar.name));
		this.camFollow.SetNewPlayer(newCurrentChar.name);

		this.camFollow.FollowPlayer();

		Destroy(this.currentCharacter);
		this.currentCharacter = GameObject.Find(newCurrentChar.name);
	}
}
