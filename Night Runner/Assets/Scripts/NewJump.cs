using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJump : MonoBehaviour



{
    float jumpHeight = 10;

    public float minJumpHeight;

    [Tooltip("How much the jump height will increase by each interval")]
    public float jumpHeightIncrease;

    public bool isGrounded;

    float jumpStartTime;
    float heldTime;

    [Tooltip("How often or quickly the jump height will increase")]
    public float increaseInterval;

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
            jumpStartTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded) // jump control
        {
            heldTime = Time.time - jumpStartTime;
            jump = true;
        }
    }
    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (jump == true)
        {
            if (heldTime < increaseInterval)
            {
                jumpHeight = minJumpHeight;
            }
            else if (heldTime >= increaseInterval && heldTime < increaseInterval * 2)
            {
                jumpHeight = minJumpHeight + jumpHeightIncrease;
            }
            else
            {
                jumpHeight = minJumpHeight + jumpHeightIncrease * 2;
            }
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            jump = false;
        }

        /*
        if (Input.GetKeyDown("w"))
        {
            startTime = Time.time;
        }
        if(Input.GetKeyUp("w"))
        {
            Debug.Log((Time.time - startTime).ToString("00:00.00"));
        }
         */
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