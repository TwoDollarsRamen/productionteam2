using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player;
    Sanity playerSanity;

    public Button resumeButton;
    public Button exitButton;
    public Text title;
    public GameObject blur;

    public AudioSource backgroundMusic;

    public RawImage background;

    bool gamePaused = false;
    bool pauseEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        playerSanity = player.GetComponent<Sanity>();

        activateObjects(false);

        resumeButton.onClick.AddListener(GameUnPause); // button to restart
        exitButton.onClick.AddListener(Exit);
    }

    void activateObjects(bool active = true) {
        resumeButton.gameObject.SetActive(active);
        exitButton.gameObject.SetActive(active);
        background.gameObject.SetActive(active);
        title.gameObject.SetActive(active);
        blur.gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false && pauseEnabled == true)
        {
            GamePause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true && pauseEnabled == true)
        {
            GameUnPause();
        }
    }
    void GamePause()
    {
		activateObjects();

        backgroundMusic.Pause();

        Time.timeScale = 0.0f; // pause
        gamePaused = true;

        playerSanity.heartBeatObjectOne.SetActive(false);
        playerSanity.heartBeatObjectTwo.SetActive(false);
        playerSanity.heartBeatObjectThree.SetActive(false);
    }
    void GameUnPause()
    {
		activateObjects(false);

        backgroundMusic.UnPause();

        Time.timeScale = 1.0f; // unpause
        gamePaused = false;

        playerSanity.heartBeatObjectOne.SetActive(true);
        playerSanity.heartBeatObjectTwo.SetActive(true);
        playerSanity.heartBeatObjectThree.SetActive(true);
    }

    void Exit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    } 
}
