using UnityEngine;
using System.Collections;

public class LinearMoveThenDestroy : MonoBehaviour {

	public float xSpawnPoint;
	public bool spawnAtInitialX;
	public float ySpawnPoint;
	public bool spawnAtInitialY;
	public float xKillPoint;
	public float yKillPoint;

	public bool moveRight;
	public bool moveLeft;
	public bool moveUp;
	public bool moveDown;

	public float speed;

	private Vector3 spawnPosition;

	void Start () {

		if (this.spawnAtInitialX && this.spawnAtInitialY) {
			this.spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		}
		else if (this.spawnAtInitialX) {
			this.spawnPosition = new Vector3(this.transform.position.x, this.ySpawnPoint, this.transform.position.z);
		}
		else if (this.spawnAtInitialY) {
			this.spawnPosition = new Vector3(this.xSpawnPoint, this.transform.position.y, this.transform.position.z);
		}
		else {
			this.spawnPosition = new Vector3(this.xSpawnPoint, this.ySpawnPoint, this.transform.position.z);
		}
	}

	void Update () {
	
		if (this.transform.position.x < this.xKillPoint && this.transform.position.y < this.yKillPoint) {
			this.objectMovement();
		}
		else {
			GameObject clone = Instantiate(this.gameObject, this.spawnPosition, Quaternion.identity) as GameObject;
			LinearMoveThenDestroy lmtd = clone.GetComponent<LinearMoveThenDestroy>();
			Destroy(this.gameObject);
			lmtd.enabled = true;
		}

	}

	private void objectMovement() {

		if (this.moveRight) {
			this.transform.Translate(Vector3.right * this.speed * Time.deltaTime);
		}
		else if (this.moveLeft) {
			this.transform.Translate(-Vector3.right * this.speed * Time.deltaTime);
		}
		else if (this.moveUp) {
			this.transform.Translate(Vector3.up * this.speed * Time.deltaTime);
		}
		else if (this.moveDown) {
			this.transform.Translate(-Vector3.up * this.speed * Time.deltaTime);
		}
		else {
			Debug.Log("You must select a direction checkbox in LinearMoveThenDestroy!");
		}
	}
}
