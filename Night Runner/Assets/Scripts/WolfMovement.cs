using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    public Camera mainCamera;
    CameraMover mover;

    public Sanity player;
    public float sanityCloseMultiplier = 1.1f;

    public bool touchingPlayer = false;

	float initialCameraOffset;

	Vector3 targetPosition;
	Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        mover = mainCamera.GetComponent<CameraMover>();

        initialCameraOffset = (transform.position - mainCamera.transform.position).x;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    	var unitSanity = (float)player.maxSanity - (float)player.sanity;

        targetPosition.x = mainCamera.transform.position.x + initialCameraOffset +
        	unitSanity * sanityCloseMultiplier;
        transform.position = targetPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }
}
