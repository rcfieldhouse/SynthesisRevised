using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystem : MonoBehaviour
{
	public float m_JumpForce = 400f;
	private bool m_Grounded = false;
	private bool m_FacingRight = true;
	private float diff; 
	private Vector3 m_Velocity = Vector3.zero;
	private float Direction = 1.0f;  
	private float MovementCooldown=0.0f;
	private GameObject target;
	private Vector3 KnockbackDir = new Vector3(0.0f, 0.0f, 0.0f);
	// Start is called before the first frame update
	void Start()
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			target = other.gameObject;
			//ew
			if (MovementCooldown < 0.0f)
			{
				diff =  other.transform.position.x- GetComponent<Rigidbody>().transform.position.x;				
				Direction =diff/ Mathf.Abs(diff); ;
				MovementCooldown = 1.0f; ;
			}
			Attack(GetComponent<Rigidbody>().transform, 1);
		}
		
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Ground")
        {
			if (MovementCooldown < 0)
			{
				Direction *= -1;
				MovementCooldown = 1.0f; ;
			}			
		}
	
	}
  
    // Update is called once per frame
    void Update()
	{
		//Direction = Input.GetAxisRaw("Horizontal") ;
		MovementCooldown -= Time.deltaTime;
		Move(Direction*5.0f, false);

	}

    public void Move(float move, bool jump)
	{


		if (jump == true)
		{
			// Add a vertical force to the player.
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().AddForce(new Vector3(0f, m_JumpForce * 10), 0f);

		}
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector3(move, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
		// And then smoothing it out and applying it to the character
		GetComponent<Rigidbody>().velocity = Vector3.SmoothDamp(GetComponent<Rigidbody>().velocity, targetVelocity, ref m_Velocity, 0.1f);

		
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}


	}
	public void Attack(Transform pos, int Dmg)
	{
		KnockbackDir = target.transform.position - pos.position;
		target.GetComponent<HealthSystem>().TakeDamage(Dmg, KnockbackDir);
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