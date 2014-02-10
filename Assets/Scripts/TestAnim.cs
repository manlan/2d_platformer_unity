using UnityEngine;
using System.Collections;

public class TestAnim : MonoBehaviour {

	private Animator anim;
	// Use this for initialization

	void Awake() {
		this.anim = GetComponent<Animator>();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (this.anim.GetCurrentAnimatorStateInfo(0));
	}
}
