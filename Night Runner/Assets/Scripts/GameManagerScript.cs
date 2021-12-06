
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    Sanity playerSanity;

    public GameObject wolf;
    WolfMovement wolfMovement;

    public GameObject finishLine;
    FinishLineScript finishScript;

    public Canvas winLoseCanvas;
    WinLoseMenu winLoseMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerSanity = player.GetComponent<Sanity>();
        wolfMovement = wolf.GetComponent<WolfMovement>();
        finishScript = finishLine.GetComponent<FinishLineScript>();
        winLoseMenu = winLoseCanvas.GetComponent<WinLoseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishScript.touchingPlayer == true) // FinishLine Win Condition
        {
            // Win
            winLoseMenu.WinLoseResult(true);
        }
        if (playerSanity.sanity < 0) // Sanity Loss Condition
        {
            // Lose
            winLoseMenu.WinLoseResult(false); // o==[}::::::::::::::::::::::::::::::>
        }
        if (player.transform.position.y < -3) // Fall in hole loss condition
        {
            // Lose
            winLoseMenu.WinLoseResult(false);
        }
        if (wolfMovement.touchingPlayer) // Touch Wolf Loss Condition
        {
            // Lose
            winLoseMenu.WinLoseResult(false);
        }
    }
}
