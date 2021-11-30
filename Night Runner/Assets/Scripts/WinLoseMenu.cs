
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class WinLoseMenu : MonoBehaviour
{
    public Button restartButton = null;
    public Button exitButton = null;
    public Image background = null;

    // Start is called before the first frame update
    void Start()
    {
        //restartButton.isActiveAndEnabled;
        //exitButton.SetActive(false);
        //background.SetActive(false);

        //background = GameObject.Find("WinLoseBackground");
        //restartButton = GameObject.Find("RestartButton");
        //exitButton = GameObject.Find("ExitButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // jump control
        {
            Time.timeScale = 0.0f; // pause
        }
    }
}
