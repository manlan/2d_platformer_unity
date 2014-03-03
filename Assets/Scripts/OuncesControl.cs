using UnityEngine;
using System.Collections;

public class OuncesControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]

	//prevented ounces from jumping - he doens't need it and it introduces bugs.
//	public bool jump = false;				// Condition for whether the player should jump.
	
	
	public float moveForce = 5f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.

	//prevented ounces from jumping - he doens't need it and it introduces bugs.
//	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool notAirbourne = false;

	private JellySprite jellySprite;
	
	
	void Awake()
	{
		groundCheck = transform.Find("groundCheck");
	}

	void Start() {

		this.jellySprite = this.GetComponent<JellySprite>();
	}
	
	
	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
		this.notAirbourne = Physics2D.Linecast(this.transform.position, this.groundCheck.position + 2 * Vector3.down, 1 << LayerMask.NameToLayer("Ground"));

		Debug.DrawLine(this.transform.position, this.groundCheck.position + 2 * Vector3.down);
//		Debug.Log (this.notAirbourne);

		//prevented ounces from jumping - he doens't need it and it introduces bugs.

//		if(Input.GetButtonDown("Jump") && grounded)
//			jump = true;
	}
	
	
	void FixedUpdate ()
	{
		// Cache the horizontal input.

		float hInput = 0f;

		if (this.notAirbourne) {

			hInput = Input.GetAxis("Horizontal");
		}
		else {
			hInput = 0;
		}


//		}
		
		//		Debug.Log (h);
		// The Speed animator parameter is set to the absolute value of the horizontal input.
//		anim.SetFloat("Speed", Mathf.Abs(h));
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
//		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.

		Vector2 staticForce = new Vector2(1f, 1.5f);
		Vector2 actualMoveForce = staticForce * hInput * this.moveForce;
//		Vector2 v2Position = new Vector2(-10,-10);

		this.jellySprite.AddForceAndClampVelocity(new Vector2(actualMoveForce.x, Mathf.Abs (actualMoveForce.y)), this.maxSpeed);

//		Debug.Log ("hInput: " + hInput + " staticForce: " + staticForce + " Actual Move Force: " + actualMoveForce);


		
		// If the player's horizontal velocity is greater than the maxSpeed...
//		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
//			this.jellySprite.rigidbody2D.velocity = new Vector2(Mathf.Sign(this.jellySprite.rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if(hInput > 0 && !facingRight) {

			Flip();

		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(hInput < 0 && facingRight) {

			Flip();
		}
		
			//prevented ounces from jumping - he doens't need it and it introduces bugs.

//		if(jump)
//		{
			// Set the Jump animator trigger parameter.


			// Add a vertical force to the player.
//			this.jellySprite.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
//			jump = false;
//		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
