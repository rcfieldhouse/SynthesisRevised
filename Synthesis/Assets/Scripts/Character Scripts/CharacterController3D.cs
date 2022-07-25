using UnityEngine;
using UnityEngine.Events;

public class CharacterController3D : MonoBehaviour
{

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
	private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround, m_WhatIsWall;                          // A mask determining what is ground to the character															//[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private BoxCollider coll; 
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded,m_WallRun;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	//private Rigidbody2D m_Rigidbody2D;
	private Rigidbody Rigidbody; 
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
	private Vector3 down = new Vector3(0.0f,0.0f,-1.0f);
	private void Awake()
	{
		Physics.gravity *= 3;
		Rigidbody = GetComponent<Rigidbody>();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

	}

	private void FixedUpdate()
	{
		
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.tag == "Ground")
    //    {
	//		m_Grounded = true;
	//	}
	//}
	//private void OnTriggerExit(Collider other)
	//{
	//	if (other.tag == "Ground")
	//	{
	//		m_Grounded = false;
	//		GetComponent<PlayerInput>().SetJump(true);
	//	}
	//}
	public void Move(float move, bool crouch, bool jump, bool doubleJump,float RunUp)
	{
		
		Debug.Log("move " + move);
		// If crouching, check to see if the character can stand up
		//if (!crouch)
		//{
		//	// If the character has a ceiling preventing them from standing up, keep them crouching
		//	if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
		//	{
		//		crouch = true;
		//	}
		//}

		//only control the player if grounded or airControl is turned on
		isGrounded();
		IsWallRun();
	
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
			if (((m_Grounded == true) && (jump == true)) || (doubleJump == true))
			{
				// Add a vertical force to the player.
				Rigidbody.velocity = Vector3.zero;
				Rigidbody.AddForce(new Vector3(0f, m_JumpForce * 10), 0f);
				m_Grounded = false;
			}
			Debug.Log("grounded" + m_Grounded);
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector3(move * 10f, Rigidbody.velocity.y, Rigidbody.velocity.z);
			if ((m_WallRun == true) && (RunUp != 0))
			{
				targetVelocity.y = 7*RunUp;
			}
			// And then smoothing it out and applying it to the character
			Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
	
		// If the player should jump...


	}
	private bool IsWallRun()
    {
		if ((Physics.BoxCast(coll.bounds.center - Vector3.left / 10, coll.bounds.size / 2, Vector3.left, Quaternion.Euler(Vector3.left), 0.1f, m_WhatIsWall) == true
			|| Physics.BoxCast(coll.bounds.center - Vector3.right / 10, coll.bounds.size / 2, Vector3.right, Quaternion.Euler(Vector3.right), 0.1f, m_WhatIsWall) == true))
		{
			m_WallRun = true;
		}
		else m_WallRun = false;	
		Debug.Log("WallContact" + m_WallRun);
		return m_WallRun;
	}
    public bool GetIfGrounded()
	{
		return isGrounded();
	}

	private bool isGrounded()
    {
		
		m_Grounded= Physics.BoxCast(coll.bounds.center-Vector3.down/10, coll.bounds.size/2, Vector3.down, Quaternion.Euler(Vector3.down),0.1f,m_WhatIsGround);
		return Physics.BoxCast(coll.bounds.center - Vector3.down / 10, coll.bounds.size/2, Vector3.down, Quaternion.Euler(Vector3.down),0.1f, m_WhatIsGround);
	}
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}