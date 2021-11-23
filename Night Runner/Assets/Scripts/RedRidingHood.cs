
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedRidingHood : MonoBehaviour
{
	public float moveForce = 10;

	Rigidbody rb;

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
	}

    private void Update()
    {
		//transform.Translate(maxSpeed * Time.deltaTime, 0, 0);

		if (rb.velocity.x < moveForce) // limit speed
		{
			rb.AddForce(new Vector3(moveForce, 0.0f, 0.0f));
		}
	}
}
