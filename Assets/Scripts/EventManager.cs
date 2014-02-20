using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void PositionHandler(float aNewPosition, bool facingRight);
	public static event PositionHandler FireAtAimedPosition;

	public static void NewAimedPosition(float aNewPosition, bool facingRight) {

		if (FireAtAimedPosition != null) {

			FireAtAimedPosition(aNewPosition, facingRight);
		}
	}

	
}
