
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseMenu : MonoBehaviour
{
    static bool restarted = false;

    public Button restartButton;
    public Button exitButton;

    public Image background;

    public Canvas startMenu;

    bool gamePaused = false;
    bool pauseEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        restartButton.onClick.AddListener(Restart); // button to restart
        exitButton.onClick.AddListener(Exit);

        if (restarted)
        {
            CloseStartMenu();
            restarted = false;
        }
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
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        Time.timeScale = 0.0f; // pause
        gamePaused = true;
    }
    void GameUnPause()
    {
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        Time.timeScale = 1.0f; // unpause
        gamePaused = false;
    }

    public void WinLoseResult(bool playerWon)
    {
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        Time.timeScale = 0.0f; // pause
        pauseEnabled = false;
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

        startMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        restarted = true;
    }
    void CloseStartMenu() // function
    {
        startMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
