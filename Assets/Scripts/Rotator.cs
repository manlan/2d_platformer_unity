using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float speed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this.transform.Rotate(Vector3.up * this.speed * Time.deltaTime);
	}
}
