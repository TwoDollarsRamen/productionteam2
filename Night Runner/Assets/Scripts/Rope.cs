using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	public GameObject segmentRoot;
	public RopeEnd endPoint;

	public RedRidingHood player;

	[Tooltip("This force will be applied to the player's rigidbody when they jump off the rope.")]
	public Vector2 jumpOffForce = new Vector2(3.0f, 3.0f);
	
	[Tooltip("The offset of the player when they snap onto the rope")]
	public Vector2 snapOffset = new Vector2(0.0f, 0.0f);

	List<Transform> segments;

	bool oldSwinging;

	void Start()
	{
		player = GameObject.Find("Red Riding Hood").GetComponent<RedRidingHood>();
		segments = new List<Transform>();

		endPoint.gameObject.GetComponent<MeshRenderer>().enabled = false;

		foreach (Transform child in segmentRoot.transform)
		{
			segments.Add(child);
		}
	}

	void Update()
	{
		float t = 0.0f;

		float tIncrement = 1.0f / (float)segments.Count;

		foreach (var segment in segments)
		{
			if (segment != segments[0])
			{
				var point0 = segments[0].position;
				var point2 = endPoint.transform.position;
				var point1 = point0 + (point2 - point0) / 2 + Vector3.down * 5.0f;
				
				var m1 = Vector3.Lerp(point0, point1, t);
				var m2 = Vector3.Lerp(point1, point2, t);
				segment.position = Vector3.Lerp(m1, m2, t);
			}

			t += tIncrement;
		}

		if (endPoint.swinging) {
			player.GetComponent<Rigidbody>().isKinematic = true;
			player.GetComponent<RedRidingHood>().enabled = false;

			player.transform.position = new Vector3(snapOffset.x, snapOffset.y, 0.0f) + endPoint.transform.position;
		} else {
			player.GetComponent<Rigidbody>().isKinematic = false;
			player.GetComponent<RedRidingHood>().enabled = true;
			
			if (oldSwinging) {
				oldSwinging = false;
				player.GetComponent<Rigidbody>().AddForce(jumpOffForce.x, jumpOffForce.y, 0, ForceMode.Impulse);
			}
		}
	}

	void OnTriggerEnter(Collider collider) 
	{
		if (collider.CompareTag("Player") == true)
		{
			if (!endPoint.swinging) {
				endPoint.Swing();
				oldSwinging = true;
			}
		}
	}
}
