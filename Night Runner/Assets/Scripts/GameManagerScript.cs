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
            wolf.SetActive(false);
        }

        if (playerSanity.sanity < 0) // Sanity Loss Condition
        {
            // Lose
            wolf.SetActive(false);
        }
        if (player.transform.position.y < -3) // Fall in hole loss condition
        {
            // Lose
            wolf.SetActive(false);
        }
        if (wolfMovement.touchingPlayer) // Touch Wolf Loss Condition
        {
            // Lose
            wolf.SetActive(false);
        }
    }
}
