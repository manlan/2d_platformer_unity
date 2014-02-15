using UnityEngine;
using System.Collections;

public class DestroyOnEnterAndRespawn : MonoBehaviour {

	private ArrayList listOfSpawns;
	private Transform nearestSpawn;
	private Camera cam;
	private CameraFollow camFollow;

	void Awake() {

		this.cam = Camera.main;
		this.camFollow = cam.GetComponent<CameraFollow>();
	}	

	void Start() {

		this.listOfSpawns = new ArrayList();
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("SpawnPoint")) {
			this.listOfSpawns.Add(g.transform);
		}

	}

//	void Update() {

//		Debug.Log (this.listOfSpawns.Count);

//	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.tag == "Player") {
//			Debug.Log("Player with name: " + collider.name);
		
			this.camFollow.FollowPlayer();
			Vector3 spawnVector = new Vector3();

			float smallestDistance = 10000f; 
			foreach (Transform spawnPoint in this.listOfSpawns) {

				float distanceToSpawn = Vector3.Distance(spawnPoint.position, collider.transform.position);

//				Debug.Log("spawnPoint name: " + spawnPoint.name + " spawnPoint position: " + spawnPoint.position + " has distance to spawn: " + distanceToSpawn);

				if (distanceToSpawn < smallestDistance) {

					smallestDistance = distanceToSpawn;
					spawnVector = spawnPoint.position;
				}
			}


//			GameObject clone = Instantiate(other.gameObject, spawnVector, Quaternion.identity) as GameObject;
//			Destroy(other.gameObject);

			ObjectPool.instance.PoolObject(collider.gameObject);
			GameObject clone = ObjectPool.instance.GetObjectForType(collider.gameObject.name, true);
			clone.transform.position = spawnVector;

			this.camFollow.SetNewPlayer(clone.name);
			this.camFollow.FollowPlayer();
		}

	}

}