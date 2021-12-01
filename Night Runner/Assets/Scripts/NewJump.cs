using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJump : MonoBehaviour
{
	[Tooltip("Extra velocity added when the player is falling")]
	public float fallMultiplier = 2.5f;
	[Tooltip("\"Brake\" velocity to stop the player's jump if he let's go of the space bar")]
	public float lowJumpMultiplier = 2.0f;
	[Tooltip("Velocity to be added to the \"Y\" axis of the player's velocity should he jump.")]
	public float jumpVelocity = 20.0f;

    public bool isGrounded;

    bool jump = false;

    Rigidbody rb; // refers to red riding hood cubes rigid body

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // jump control
        {
            jump = true;
        }
    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {	
			rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}

        if (jump)
        {
        	rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            jump = false;
        }
    }
    /// <summary>
    /// If the bottom of the player collides with an object, set isGrounded to true
    /// </summary>
    void OnCollisionStay(Collision collidedObject)
    {
        ContactPoint contact = collidedObject.contacts[0];
        if (Vector3.Dot(contact.normal, Vector3.up) > 0.5) // checks if contact was below
        {
            isGrounded = true;
        }
    }
    /*
    void OnCollisionStay() // option if you want 'wall jumping'
    {
        isGrounded = true;
    }
    */
    /// <summary>
    /// Whenever the player stops colliding with the floor, allow them to jump again.
    /// </summary>
    void OnCollisionExit()
    {
        isGrounded = false;
    }
}
