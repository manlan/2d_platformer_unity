using UnityEngine;
using System.Collections;

public class DestroyOnEnter : MonoBehaviour {

	private ArrayList listOfSpawns;
	private Transform nearestSpawn;
	private Camera cam;
	private CameraFollow camFollow;

	void Awake() {

		this.listOfSpawns = new ArrayList();
		this.cam = Camera.main;
		this.camFollow = cam.GetComponent<CameraFollow>();
	}	

	void Start() {

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("SpawnPoint")) {
			this.listOfSpawns.Add(g.transform);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
		
			this.camFollow.FollowPlayer();
			Vector3 spawnVector = new Vector3();
			foreach (Transform t in this.listOfSpawns) {
				float smallestDistance = 10000f; 
				float distanceToSpawn = Vector3.Distance(t.position, other.transform.position);

				if (distanceToSpawn < smallestDistance) {
					smallestDistance = distanceToSpawn;
					spawnVector = t.position;
				}
			}


			GameObject clone = Instantiate(other.gameObject, spawnVector, Quaternion.identity) as GameObject;
			Destroy(other.gameObject);

			this.camFollow.SetNewPlayer(clone.name);
			this.camFollow.FollowPlayer();
		}

	}

}