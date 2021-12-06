using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeJoint : MonoBehaviour
{
	public Transform segment;

	void Update() {
		transform.position = segment.position;
	}
}
