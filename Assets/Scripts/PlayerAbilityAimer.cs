using UnityEngine;
using System.Collections;

public class PlayerAbilityAimer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	public string aimButtonName;
	public float speed;
	
//	private Transform player;		// Reference to the player.

	private bool isVertical = false;
	private bool isHorizontal = false;

	void Start () {
 		
		this.renderer.enabled = false;
	}

	private IEnumerator CheckAim () {

		yield return new WaitForEndOfFrame();

//		Debug.Log ("Is player active? " + this.isPlayerActive);
//		Debug.Log ("it's active before the while " + this.gameObject.activeInHierarchy);

		while (true) {

//			Debug.Log ("This aimbar color in the CheckAim:" + this.aimBar.color.a);
			if (Input.GetButton(this.aimButtonName)) {

				Aimer();
//				StartCoroutine(FadeOut(1f, 0f));
			}

			if (Input.GetButtonUp(this.aimButtonName)) {

				StartCoroutine(FadeOut(0f, 2f));

			}

			yield return null;
		}

//		Debug.Log("Test");
	}

	IEnumerator FadeOut(float aValue, float aTime)
	{
		float alpha = transform.renderer.material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			transform.renderer.material.color = newColor;
			yield return null;
		}
		ResetRotation();
	}
	
	void Aimer () {

		this.renderer.enabled = true;
		Color opaqueColour = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		this.renderer.material.color = opaqueColour;

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

	void ResetRotation() {

		this.transform.eulerAngles = new Vector3(0, 0, 0);
	}


	public void StartCheckAim() {

		ResetRotation();
		StartCoroutine("CheckAim");
	}

	public void StopCheckAim() {

		StopCoroutine("CheckAim");
	}
}
