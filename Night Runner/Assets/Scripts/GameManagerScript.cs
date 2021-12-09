
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    Sanity playerSanity;

    public GameObject wolf;
    WolfMovement wolfMovement;

    [Tooltip("How far beind the middle of the wolf the player can be before they lose. A positive number will push the offset ahead of the center of the wolf and a negative will push it behind the center.")]
    public float loseIfBehindOffset;

    public GameObject finishLine;
    FinishLineScript finishScript;

    public Canvas winLoseCanvas;
    WinLoseMenu winLoseMenu;

    public bool allowRToRestart = false;

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
        /*if (playerSanity.sanity < 0) // Sanity Loss Condition
        {
            // Lose
            winLoseMenu.WinLoseResult(false); // o==[}::::::::::::::::::::::::::::::>
        }*/
        if (player.transform.position.y < -3) // Fall in hole loss condition
        {
            // Lose
            winLoseMenu.WinLoseResult(false);

            playerSanity.heartBeatObjectOne.SetActive(false);
            playerSanity.heartBeatObjectTwo.SetActive(false);
            playerSanity.heartBeatObjectThree.SetActive(false);
        }
        if (wolfMovement.touchingPlayer) // Touch Wolf Loss Condition
        {
            // Lose
            winLoseMenu.WinLoseResult(false);

            playerSanity.heartBeatObjectOne.SetActive(false);
            playerSanity.heartBeatObjectTwo.SetActive(false);
            playerSanity.heartBeatObjectThree.SetActive(false);
        }
        if (player.transform.position.x < wolfMovement.transform.position.x + loseIfBehindOffset) // Fall behind wolf
        {
            // Lose
            winLoseMenu.WinLoseResult(false);

            playerSanity.heartBeatObjectOne.SetActive(false);
            playerSanity.heartBeatObjectTwo.SetActive(false);
            playerSanity.heartBeatObjectThree.SetActive(false);
        }

        if (allowRToRestart && Input.GetKeyDown(KeyCode.R)) {
        	var currentSceneName = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene(currentSceneName);
        }
    }
}
