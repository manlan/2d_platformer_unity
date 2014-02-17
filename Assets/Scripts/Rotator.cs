using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public int speed;

	void Start () {
	
	}

	void Update () {

		foreach (Transform childTransforms in this.transform) {

			childTransforms.transform.Rotate(Vector3.up * this.speed * Time.deltaTime);
		}
	}
}
