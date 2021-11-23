using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	public GameObject segmentRoot;
	public RopeEnd endPoint;

	public float sag = 0.5f;

	List<Transform> segments;

	void Start()
	{
		segments = new List<Transform>();

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

		print(t);
	}
}
