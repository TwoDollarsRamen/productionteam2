using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Sanity playerSanity;

    public GameObject wolf;
    WolfMovement wolfMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerSanity = player.GetComponent<Sanity>();
        wolfMovement = wolf.GetComponent<WolfMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSanity.sanity <= 0) // Sanity Loss Condition
        {
            // Lose
        }
        if (player.transform.position.x < 0) // Fall in hole loss condition
        {
            // Lose
        }
        if (wolfMovement.touchingPlayer) // Touch Wolf Loss Condition
        {
            // Lose
        }
    }
}
