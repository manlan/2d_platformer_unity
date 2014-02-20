using UnityEngine;
using System.Collections;

public class PlayerAbilityAimer : MonoBehaviour
{
	public string aimButtonName;
	public float speed;
	public float timeBeforeNextAim;



	private bool isVertical = false;
	private bool isHorizontal = false;
	private float rotatedBy;

	void Start () {
 		
		this.renderer.enabled = false;
	}

	private IEnumerator CheckAim () {

		yield return new WaitForEndOfFrame();

//		Debug.Log ("Is player active? " + this.isPlayerActive);
//		Debug.Log ("it's active before the while " + this.gameObject.activeInHierarchy);

		while (true) {
//			Debug.Log (this.transform.root.localScale.x > 0);
//			Debug.Log ("This aimbar color in the CheckAim:" + this.aimBar.color.a);
			if (Input.GetButton(this.aimButtonName)) {

				RenderOnAndOpaque();
				Aimer();
			}

			if (Input.GetButtonUp(this.aimButtonName)) {

				StartCoroutine(FadeOut(0f, .5f));
				yield return new WaitForSeconds(.1f);

				StoreLocation();
				ResetRotation();
				RenderOff();
				yield return new WaitForSeconds(this.timeBeforeNextAim);
			}
			yield return null;
		}
	}

	IEnumerator FadeOut(float aValue, float aTime) {

		float alpha = this.transform.renderer.material.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime){

			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			transform.renderer.material.color = newColor;
			yield return null;
		}

	}
	
	void Aimer () {

		if (this.transform.eulerAngles.z <= 100 && !isVertical) {

			this.transform.Rotate(Vector3.forward, Time.deltaTime * this.speed);

			if (this.transform.eulerAngles.z >= 90) {

				isVertical = true;
				isHorizontal = false;
			}
		}
		else if (this.transform.eulerAngles.z >= 0 && !isHorizontal) {

			this.transform.Rotate(Vector3.back, Time.deltaTime * this.speed);

			if (this.transform.eulerAngles.z <= 10) {

				isHorizontal = true;
				isVertical = false;
			}
		}
	}

	void RenderOnAndOpaque() {

		this.renderer.enabled = true;
		Color opaqueColour = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		
		this.renderer.material.color = opaqueColour;
	} 

	void RenderOff() {
		
		this.renderer.enabled = false;
	} 

	void StoreLocation() {

		this.rotatedBy = this.transform.eulerAngles.z;
		EventManager.NewAimedPosition(this.rotatedBy, this.transform.root.localScale.x > 0);
//		Debug.Log ("z rotation angles are: " + this.rotatedBy + " and z euler angles are: " + this.transform.eulerAngles.z);
		
	}

	void ResetRotation() {

		this.transform.eulerAngles = new Vector3(0, 0, 0);
		this.isVertical = false;
		this.isHorizontal = false;
	}


	public void StartCheckAimAndReset() {

//		EventManager.FireAtAimedPosition += StoreLocation;
		ResetRotation();
		RenderOff();
		StartCoroutine("CheckAim");
	}

	public void StopCheckAimAndReset() {

//		EventManager.FireAtAimedPosition -= StoreLocation;
		ResetRotation();
		RenderOff();
		StopCoroutine("CheckAim");
	}
}
