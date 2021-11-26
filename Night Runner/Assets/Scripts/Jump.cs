using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpHeight = 10;

    public bool isGrounded;

    bool jump = false;

    Rigidbody rb; // refers to red riding hood cubes rigid body

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (jump == true)
        {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            jump = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // jump control
        {
            jump = true;
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
