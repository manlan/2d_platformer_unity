using UnityEngine;
using System.Collections;

public class OuncesAbility : PlayerAbility {

	public float forceStrength;

	void OnEnable() {

		EventManager.FireAtAimedPosition += NewAimedPosition;
	}

	void OnDisable() {

		EventManager.FireAtAimedPosition -= NewAimedPosition;
	}


	public override void NewAimedPosition(float aNewPosition, bool facingRight) {

		//propel ounces by the force in aNewPosition
//		
//		aNewPosition is in format transform.eulerangles.z

		float y = Mathf.Sin (aNewPosition * Mathf.Deg2Rad);
		float x;
		if (facingRight) {
			x = Mathf.Cos (aNewPosition * Mathf.Deg2Rad);
		}
		else {
			x = Mathf.Cos (aNewPosition * Mathf.Deg2Rad) * -1f;
		}

//
		float yFinal = y * this.forceStrength; //* Mathf.Rad2Deg;
		float xFinal = x * this.forceStrength; //Mathf.Rad2Deg;

		Vector2 preImpuseForce = new Vector2(xFinal, yFinal);
		preImpuseForce /= Time.fixedDeltaTime;

//		float fourtyFiveDegToRads = 56 * Mathf.Deg2Rad;

		this.jellySprite.AddForce(preImpuseForce);
//		Debug.Log ("xFinal: " + xFinal + " yFinal: " + yFinal);
//		Debug.Log ("z euler we feed is: " + aNewPosition);
//		Debug.Log ("cos56: " + Mathf.Sin(56 * Mathf.Deg2Rad) + " sin56: " + Mathf.Sin (56) + " Sin56*degToRad: " + (Mathf.Sin (56)) * (Mathf.Rad2Deg));
	}
}
