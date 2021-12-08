using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseMenu : MonoBehaviour
{
    static bool restarted = false;

    public RedRidingHood player;
    Sanity playerSanity;

    public Button restartButton;
    public Button exitButton;
    public Text gameOverText;
    public string gameOverLoseDisplay = "Y O U  W I N";
    public string gameOverWinDisplay = "Y O U  L O S E";

    public GameObject blur;
    public GameObject pauseMenu;

    public AudioSource backgroundMusic;
    public AudioSource menuAmbience;

    public RawImage background;

    public Canvas startMenu;

    // Start is called before the first frame update
    void Start()
    {
		activateObjects(false);

    	restartButton.onClick.AddListener(Restart); // button to restart
        exitButton.onClick.AddListener(Exit);

        playerSanity = player.GetComponent<Sanity>();

        if (restarted)
        {
            CloseStartMenu();
            restarted = false;
            backgroundMusic.Play();
            playerSanity.heartBeatObjectOne.SetActive(true);
            playerSanity.heartBeatObjectTwo.SetActive(true);
            playerSanity.heartBeatObjectThree.SetActive(true);
            menuAmbience.Stop();
        }
    }

    void activateObjects(bool active = true) {
        restartButton.gameObject.SetActive(active);
        exitButton.gameObject.SetActive(active);
        background.gameObject.SetActive(active);
        gameOverText.gameObject.SetActive(active);
        blur.SetActive(active);
    }

    public void WinLoseResult(bool playerWon)
    {
    	pauseMenu.SetActive(false);
		activateObjects();

        Time.timeScale = 0.0f; // pause
        backgroundMusic.Stop();
        Cursor.visible = true;

        gameOverText.text = playerWon ? gameOverWinDisplay : gameOverLoseDisplay;
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
    void Restart() // function
    {
        Scene scene = SceneManager.GetActiveScene(); // Scene reload
        SceneManager.LoadScene(scene.name);

    	pauseMenu.SetActive(true);

        startMenu.gameObject.SetActive(false);
        backgroundMusic.Play();
        Time.timeScale = 1.0f;
        restarted = true;
        Cursor.visible = false;
    }
    void CloseStartMenu() // function
    {
        startMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }
}
