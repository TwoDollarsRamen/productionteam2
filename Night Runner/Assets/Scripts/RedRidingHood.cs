
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedRidingHood : MonoBehaviour
{
	public Camera mainCamera;
	CameraMover mover;

	[Tooltip("How far back from the middle of the camera the player runs")]
	public float chaseDisplacement = 4;

	[Tooltip("How soon the player starts slowing down")]
	public float speedMatchDisplacement = 1;

	bool catchingUp;
	float moveAccel;

	[Tooltip("How fast the player will get up to speed")]
	public float catchUpAccel;

	[Tooltip("How quickly the player will slow when reaching camera speed")]
	public float slowAccel = -1;

	[Tooltip("Max speed relative to the camera")]
	public float maxRelativeSpeed = 2; // relative to the camera

	[Tooltip("Slows the character when too far ahead of the camera")]
	public float overshootSlowAccel = -0.5f; 

	Rigidbody rb;

	void Start()
	{
		catchingUp = false;

		mover = mainCamera.GetComponent<CameraMover>();
		rb = gameObject.GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{
		if (rb.position.x < mainCamera.transform.position.x - (chaseDisplacement + speedMatchDisplacement))
        {
			moveAccel = catchUpAccel;
			catchingUp = true;
        }
        else
        {
			moveAccel = slowAccel;
			catchingUp = false;
        }

		if (catchingUp == true)
        {
			if (rb.velocity.x < maxRelativeSpeed + mover.cameraSpeed)
			{
				rb.AddForce(new Vector3(moveAccel, 0.0f, 0.0f), ForceMode.Acceleration);
			}
		}
		else
        {
			if (rb.velocity.x >= mover.cameraSpeed)
			{
				rb.AddForce(new Vector3(moveAccel, 0.0f, 0.0f), ForceMode.Acceleration);
			}
		}

		if (rb.position.x > mainCamera.transform.position.x) // slow if too far
		{
			rb.AddForce(new Vector3(overshootSlowAccel, 0.0f, 0.0f), ForceMode.Acceleration);
		}
	}