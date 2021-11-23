
using UnityEngine;

public class CameraMover : MonoBehaviour
{
	public float cameraSpeed = 1;

	void Update() 
	{
		transform.Translate(cameraSpeed * Time.deltaTime, 0, 0);
		/*
		transform.position = new Vector3(
			player.position.x,
			transform.position.y,
			transform.position.z);
		*/
	}
}
