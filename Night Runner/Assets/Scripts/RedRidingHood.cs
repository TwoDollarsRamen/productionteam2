
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedRidingHood : MonoBehaviour
{
	public Camera mainCamera;
	CameraMover mover;

	public float chaseDisplacement = 4;
	public float speedMatchDisplacement = 1;

	bool catchingUp;
	float moveAccel;
	public float catchUpAccel;
	public float slowAccel = -1;

	public float maxRelativeSpeed = 2; // relative to the camera

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
	}
}
