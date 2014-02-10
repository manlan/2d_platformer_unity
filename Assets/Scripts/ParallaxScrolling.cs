using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour {

	private Transform camera;
	public float parallax;
	public float parallaxReduction;
	public GameObject[] backgrounds;
	
	void Awake() {

		this.camera = Camera.main.transform;
	}

	void Update () {

		for (int i = 0; i < this.backgrounds.Length; i++) {
			backgrounds[i].renderer.material.mainTextureOffset = new Vector2(this.parallax * (i * this.parallaxReduction) * this.camera.position.x, 0);
		}
	}
}
