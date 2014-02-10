using UnityEngine;
using System;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xGrace = 1f;		// Distance player can move before the camera follows.
	public float yGrace = 1f;		// ""
	public float xAccel = 2f;		// How quickly the camera accelerates to catch the player
	public float yAccel = 2f;		// ""
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	private bool followSwitch = true;//Switching the update to follow the player or not.
	private GameObject player;		// Reference to the player's transform.


	void Start ()
	{
		// Setting up the reference.
		this.player = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(CoUpdate());
	}


	private bool CheckxGrace() {

		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(this.transform.position.x - this.player.transform.position.x) > xGrace;
	}


	private bool CheckyGrace() {

		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(this.transform.position.y - this.player.transform.position.y) > yGrace;
	}


	IEnumerator CoUpdate ()
	{
		while (true) {
			if(followSwitch) {
				TrackPlayer(); 	
			}
			yield return null;
			}


	}

	public void FollowPlayer() {

		this.followSwitch = !this.followSwitch;
	}

	void TrackPlayer ()
	{

		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = this.transform.position.x;
		float targetY = this.transform.position.y;


		// If the player has moved beyond the x margin...
		if(CheckxGrace())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(this.transform.position.x, this.player.transform.position.x, this.xAccel * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckyGrace())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(this.transform.position.y, this.player.transform.position.y, this.yAccel * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, this.minXAndY.x, this.maxXAndY.x);
		targetY = Mathf.Clamp(targetY, this.minXAndY.y, this.maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		this.transform.position = new Vector3(targetX, targetY, transform.position.z);
	}

	public IEnumerator SetNewPlayer(String newPlayer) {

		this.player = GameObject.Find(newPlayer);
		yield return new WaitForSeconds(.1f);
//		Debug.Log (this.player.name);
	}
}
