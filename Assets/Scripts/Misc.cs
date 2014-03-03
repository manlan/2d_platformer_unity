using UnityEngine;
using System.Collections;

public class Misc : MonoBehaviour {

	public Vector2 ApplyForceMode(Vector2 force, ForceMode forceMode)
		
	{
		
		switch (forceMode)
			
		{
			
		case ForceMode.Force:
			
			return force;
			
		case ForceMode.Impulse:
			
			return force / Time.fixedDeltaTime;
			
		case ForceMode.Acceleration:
			
			return force * rigidbody2D.mass;
			
		case ForceMode.VelocityChange:
			
			return force * rigidbody2D.mass / Time.fixedDeltaTime;

		default:
			return force;

			
		}
		
	}
}
