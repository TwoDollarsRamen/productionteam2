using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    public Camera mainCamera;
    CameraMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = mainCamera.GetComponent<CameraMover>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(mover.cameraSpeed * Time.deltaTime, 0, 0);
    }
}
