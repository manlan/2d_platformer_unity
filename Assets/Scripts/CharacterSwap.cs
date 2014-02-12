using UnityEngine;
using System.Collections;

public class CharacterSwap : MonoBehaviour {

	public GameObject explosion;
	public KeyCode swapKey;
	public float timeBeforeNextSwap;
	public GameObject[] characters;

	private GameObject currentCharacter;
	private int currentCharacterID;
	private Transform cam;
	private CameraFollow camFollow;
	private BaseManager bm;

	void Awake() {

		this.bm = GetComponent<BaseManager>();
		this.cam = Camera.main.transform;
		this.camFollow = cam.GetComponent<CameraFollow>();
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

		yield return StartCoroutine(this.camFollow.SetNewPlayer(newCurrentChar.name));

		this.camFollow.FollowPlayer();

		Destroy(this.currentCharacter);
		this.currentCharacter = GameObject.Find(newCurrentChar.name);
	}
}
