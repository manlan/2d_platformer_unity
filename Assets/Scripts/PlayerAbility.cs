using UnityEngine;
using System.Collections;

public abstract class PlayerAbility : MonoBehaviour {

	protected JellySprite jellySprite;

	void Start () {

		this.jellySprite = this.GetComponent<JellySprite>();
//		EventManager.FireAtAimedPosition += NewAimedPosition;
	}



	public abstract void NewAimedPosition(float aNewPosition, bool facingRight);

}
