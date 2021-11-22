
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedRidingHood : MonoBehaviour
{
	public float moveForce = 7;

	Rigidbody rb;

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();

		rb.AddForce(new Vector3(moveForce, 0.0f, 0.0f));
	}
}
