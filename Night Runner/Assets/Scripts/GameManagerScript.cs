using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    bool test = false;

    public GameObject player;
    Sanity playerSanity;
 
    public GameObject wolf;
    WolfMovement wolfMovement;

    public GameObject finishLine;
    FinishLineScript finishScript;

    // Start is called before the first frame update
    void Start()
    {
        playerSanity = player.GetComponent<Sanity>();
        wolfMovement = wolf.GetComponent<WolfMovement>();
        finishScript = finishLine.GetComponent<FinishLineScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishScript.touchingPlayer == true) // FinishLine Win Condition
        {
            // Win
            Time.timeScale = 0.0f; // game speed or un-pause
        }

        /* if (playerSanity.sanity < 0) // Sanity Loss Condition
        {
            // Lose
            Time.timeScale = 0.0f; // game speed or un-pause
        } */
        if (player.transform.position.y < -3) // Fall in hole loss condition
        {
            // Lose
            Time.timeScale = 0.0f; // game speed or un-pause
        }
        if (wolfMovement.touchingPlayer) // Touch Wolf Loss Condition
        {
            // Lose
            Time.timeScale = 0.0f; // game speed or un-pause
        }
    }
}
