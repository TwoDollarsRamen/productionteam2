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

    public float wolfMaxY;

    public float wolfStartingY;

    public float wolfMinY;

    [Tooltip("Recommended starting speed for your fiddling is 1.")]
    public float wolfHoverSpeed;

    public Animator anim;

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

        // wolf hover up and down
        if (transform.position.y > wolfMaxY || transform.position.y < wolfMinY)
        {
            wolfHoverSpeed = wolfHoverSpeed * -1;
        }

        targetPosition.y += wolfHoverSpeed * Time.deltaTime;

        // movement
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("finish hits");
            wolfMaxY = 100;
            wolfHoverSpeed = 10;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("catchRed");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.ResetTrigger("catchRed");
        }
    }
}
