using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJump : MonoBehaviour
{
    public float maxJumpHeight;

    [Tooltip("How fast you go down after a jump. Must be negative.")]
    public float jumpFallSpeed;

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
        if (Input.GetKeyUp(KeyCode.Space)) // jump control
        {
            rb.AddForce(0, jumpFallSpeed, 0, ForceMode.Impulse);
        }
    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (jump == true)
        {
            rb.AddForce(0, maxJumpHeight, 0, ForceMode.Impulse);
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