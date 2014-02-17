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

	private PlayerAbilityAimer pam;


	void Awake() {
	
		this.cam = Camera.main.transform;
		this.camFollow = cam.GetComponent<CameraFollow>();
		this.currentCharacterID = 0;
	}

	void Start () {


		Vector3 characterPosition = new Vector3(this.cam.position.x, this.cam.position.y, 0);

		this.currentCharacter = ObjectPool.instance.GetObjectForType(this.characters[0].name, true);
		this.currentCharacter.transform.position = characterPosition;

//		this.pam = this.currentCharacter.GetComponent<PlayerAbilityAimer>();
		GetAimerFromNewCharAndStart();

		StartCoroutine(CheckSwapChars());
	}

	public void GetAimerFromNewCharAndStart() {

		this.pam = this.currentCharacter.GetComponentInChildren<PlayerAbilityAimer>();
//		Debug.Log (this.pam.name);
		this.pam.StartCheckAim();
	}

	IEnumerator CheckSwapChars() {

		yield return new WaitForEndOfFrame();

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

		this.camFollow.FollowPlayer();

		ObjectPool.instance.PoolObject(this.currentCharacter);

		GameObject swapExplosion = ObjectPool.instance.GetObjectForType ("Player Explosion", true);
		swapExplosion.transform.position = currentCharPosition;

		AudioHelper.CreatePlayAudioObject(BaseManager.instance.characterSwap, .5f);

		yield return new WaitForSeconds(1.25f);

		this.pam.StopCheckAim();

		ObjectPool.instance.PoolObject(swapExplosion);

		this.currentCharacterID++;
		GameObject newCurrentChar;

		switch (this.currentCharacterID) {

			//Activate Ounces, Ashe was out.
			case 0: newCurrentChar = ObjectPool.instance.GetObjectForType(this.characters[0].name, true);
			newCurrentChar.transform.position = currentCharPosition;
			break;

			//Activate Fraser, Ounces was out.
			case 1: newCurrentChar = ObjectPool.instance.GetObjectForType(this.characters[1].name, true);
			newCurrentChar.transform.position = currentCharPosition;
//			this.pam
			break;

			//Activate Ashe, Fraser was out.
			case 2: newCurrentChar = ObjectPool.instance.GetObjectForType(this.characters[2].name, true);
			newCurrentChar.transform.position = currentCharPosition;
			break;

			//Activate Ounces, Ashe was out.
			default: this.currentCharacterID = 0;
			newCurrentChar = ObjectPool.instance.GetObjectForType(this.characters[0].name, true);
			newCurrentChar.transform.position = currentCharPosition;
			break;
		}

		this.camFollow.SetNewPlayer(newCurrentChar.name);
		this.camFollow.FollowPlayer();

		this.currentCharacter = GameObject.Find(newCurrentChar.name);
//		this.currentCharacter = this.characters[this.currentCharacterID];
//		this.currentCharacter.transform.position = currentCharPosition;
//		Debug.Log("Current char: " + this.currentCharacter + " current char ID: " + this.currentCharacterID);
		GetAimerFromNewCharAndStart();

	}
}
