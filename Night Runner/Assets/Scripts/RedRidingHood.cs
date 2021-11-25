
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

	//public AudioClip[] footsteps;
	public AudioSource hoodNoise;
	public AudioClip[] hoodClip;
	//private int t;
	//public GameObject jump;
	//public float footstepSpeed = 0.2f;

	Rigidbody rb;

	void Start()
	{
		catchingUp = false;
		//InvokeRepeating("RidingSteps", 0f, footstepSpeed);
		//StartCoroutine(RidingSteps());
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
		//t = Random.Range(0, 3);
		//jump.GetComponent<Jump>();
		
	}
    /*IEnumerator RidingSteps()
    {
		
		hoodNoise.clip = footsteps[t];
		hoodNoise.Play();
		yield return new WaitForSeconds(1f);
	}
	*/
    /*void RidingSteps()
    {
		if (jump.GetComponent<Jump>().isGrounded == true)
		{
			hoodNoise.clip = footsteps[t];
			hoodNoise.Play();
		}
	}*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pit")
        {
			hoodNoise.clip = hoodClip[0];
			hoodNoise.Play();
        }
		
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
			hoodNoise.clip = hoodClip[2];
			hoodNoise.Play();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Floor")
		{
			hoodNoise.clip = hoodClip[1];
			hoodNoise.Play();
		}
	}

}

