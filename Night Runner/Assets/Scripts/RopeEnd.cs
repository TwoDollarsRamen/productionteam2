using UnityEngine;

/* This script uses a Quadratic Bezier Curve to move the end of the rope
 * in an arc.
 */

public class RopeEnd : MonoBehaviour
{
	public GameObject point0;
	public GameObject point1;
	public GameObject point2;

	public float speed = 1.0f;

	Vector3 p0;
	Vector3 p1;
	Vector3 p2;

	float offset = 0.0f;

	[HideInInspector]
	public bool swinging = false;

	public bool reverse = true;

	void Start()
	{
		p0 = point0.GetComponent<Transform>().position;
		p1 = point1.GetComponent<Transform>().position;
		p2 = point2.GetComponent<Transform>().position;

		if (reverse)
			transform.position = p2;
		else
			transform.position = p0;
	}

	void Update() {
		if (swinging)
		{
			if (reverse)
			{
				if (offset > 0.0f)
					offset -= speed * Time.deltaTime;
				else
					swinging = false;
			}
			else
			{
				if (offset < 1.0f)
					offset += speed * Time.deltaTime;
				else
					swinging = false;
			}

			var m1 = Vector3.Lerp(p0, p1, offset);
			var m2 = Vector3.Lerp(p1, p2, offset);

			transform.position = Vector3.Lerp(m1, m2, offset);
		}
	}

	public void Swing()
	{
		offset = reverse ? 1.0f : 0.0f;
		swinging = true;
	}
}
