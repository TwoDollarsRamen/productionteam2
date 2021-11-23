

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedRidingHood : MonoBehaviour
{
	public float moveForce;
	public Camera mainCamera;

	bool catchingUp;
	public float catchUpForce;

	Rigidbody rb;

	void Start()
	{
		catchingUp = false;
		rb = gameObject.GetComponent<Rigidbody>();
	}
	// transform.Translate(cameraSpeed * Time.deltaTime, 0, 0);
	private void Update()
	{
		//transform.Translate(maxSpeed * Time.deltaTime, 0, 0);

		if (transform.position.x < mainCamera.transform.position.x - 4 && catchingUp == false)
		{
			moveForce += catchUpForce;
			catchingUp = true;
		}
		if (catchingUp == true)
		{
			if (transform.position.x >= mainCamera.transform.position.x - 4)
			{
				moveForce -= catchUpForce;
				catchingUp = false;
			}
		}

		if (rb.velocity.x < moveForce) // limit speed
		{
			rb.AddForce(new Vector3(moveForce, 0.0f, 0.0f));
		}
	}
}
