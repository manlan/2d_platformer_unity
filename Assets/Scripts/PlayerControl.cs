using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public float playerWidth = 1f;
	public Vector2 wallJumpForce = Vector2.zero;

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

	private bool wallJump = false;
	private bool canWallJump = false;
	private float canWallJumpTime = 0f;
	private List<Vector2> rays;
	private bool nullHVelocity = false;
	private bool facingRightUponWallHit;
//	private List<RaycastHit2D> wallHits;


	void Awake() {

		groundCheck = transform.Find("groundCheck");

		anim = GetComponent<Animator>();
	}

	void Start() {

//		this.wallCheckRay = new Ray2D(this.transform.position, Vector2.right);
//		this.rigidPlayer = this.rigidbody2D;
		this.rays = new List<Vector2>(new Vector2[] 
		                                       {new Vector2(1,0),new Vector2(1,1),new Vector2(0,1),new Vector2(-1,1),new Vector2(-1,0)});

//		this.wallHits = new List<RaycastHit2D>(5);
		}

	void Update() {

		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		this.grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded) {

			this.jump = true;
		}

		if (Time.time - this.canWallJumpTime > 0.25f) {

			this.canWallJump = false;
			this.nullHVelocity = false;
		}

		if (Input.GetButtonDown("Jump") && this.canWallJump) {

			this.wallJump = true;
		}

//		Debug.Log ("WallJump: " + this.wallJump + " CanWallJump: " + this.canWallJump);
	}
	
	void FixedUpdate ()	{
	
//		Vector2 rayDirection = new Vector2(1f, 0f);



		float h = Input.GetAxis("Horizontal");

//		if (!this.facingRight) rayDirection *= -1;

		for( int i = 0; i < this.rays.Count; i++) { 

//			Debug.Log(this.rays.Count);
			//vector2 direction of current ray
			Vector2 currentRay = this.rays[i];
			RaycastHit2D currentRayCast = Physics2D.Raycast(this.transform.position, currentRay, this.playerWidth, 1 << LayerMask.NameToLayer("Wall"));
//			this.wallHits.Add(currentRayCast); 

			Vector3 endLine = this.transform.position + (new Vector3(currentRay.x, currentRay.y, this.transform.position.z));
			Debug.DrawLine(this.transform.position, endLine, Color.red, 1.5f);

			if (currentRayCast) {

				this.facingRightUponWallHit = this.facingRight;

				if (!this.grounded) {
					
					this.canWallJump = true;
					this.canWallJumpTime = Time.time;
					if (facingRightUponWallHit == this.facingRight) this.nullHVelocity = true;
				}
				if (this.grounded) {

					this.canWallJump = false;
				}

				Debug.Log ("facingRightUponWallHit: " + facingRightUponWallHit + " facingRight: " + this.facingRight + " nullHVel: " + this.nullHVelocity + " grounded: " + this.grounded);
			}

		}

		if (h * this.rigidbody2D.velocity.x < this.maxSpeed && !this.nullHVelocity) {
			
			this.rigidbody2D.AddForce(Vector2.right * h * this.moveForce);
		}


		



		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...


		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if (this.jump) {
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}

		if (this.wallJump) {

			Vector2 wallJumpForceImpulse = this.wallJumpForce;
			wallJumpForceImpulse /= Time.fixedDeltaTime;

//			anim.SetTrigger("Jump");

			if (facingRight) this.rigidbody2D.AddForce(new Vector2(wallJumpForceImpulse.x, wallJumpForceImpulse.y));
			else this.rigidbody2D.AddForce(new Vector2(wallJumpForceImpulse.x * -1, wallJumpForceImpulse.y));

			this.wallJump = false;
		}
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
