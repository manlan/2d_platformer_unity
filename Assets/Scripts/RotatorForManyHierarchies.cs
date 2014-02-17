using UnityEngine;
using System.Collections;

public class RotatorForManyHierarchies : MonoBehaviour {

	public bool applyToChildrenOnly = true;
	private Transform[] childrenTransforms;
	private ArrayList childrenTransformsAL;
	
	public float speed = 50;
	// Use this for initialization
	void Start () {
	
		this.childrenTransforms = GetComponentsInChildren<Transform>(); 
		this.childrenTransformsAL = new ArrayList(this.childrenTransforms);

		if (this.applyToChildrenOnly) {

			this.childrenTransformsAL.RemoveAt(0);
		}

		StartCoroutine(CoUpdate());

//		Debug.Log (this.childrenTransforms[0].name);
//		}
	}
	
	// Update is called once per frame
	IEnumerator CoUpdate () {
	

		while (true) {
			foreach (Transform childTransform in this.childrenTransformsAL) {
		
			childTransform.Rotate(Vector3.up * this.speed * Time.deltaTime);
				yield return null;
			}
//		}
//		else {
//
//		this.transform.Rotate(Vector3.up * this.speed * Time.deltaTime);
		}
	}
}
