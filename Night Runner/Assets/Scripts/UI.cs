using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Establish game objects
    public GameObject play = null;
    public GameObject credits = null;
    public GameObject exit = null;
    public GameObject menu = null;
    public GameObject creditsText = null;
    public GameObject backButton = null;

    void Start()
    {
        // Establish each game object in the code
        play = GameObject.Find("Play");
        credits = GameObject.Find("Credits");
        exit = GameObject.Find("Exit");
        creditsText = GameObject.Find("CreditsText");
        menu = GameObject.Find("Menu");
        backButton = GameObject.Find("BackButton");

        // Sets the active state of each of all UI elements
        play.SetActive(true);
        credits.SetActive(true);
        exit.SetActive(true);
        menu.SetActive(true);

        creditsText.SetActive(false);
        backButton.SetActive(false);
    }

    // Plays the game
    public void Play()
    {
        Debug.Log("Play");
    }

    // Opens the credits menu
    public void CreditsText()
    {
        Debug.Log("Credits");

        // Disable all main menu UI
        play.SetActive(false);
        credits.SetActive(false);
        exit.SetActive(false);
        menu.SetActive(false);

        // Enable all credits menu UI
        creditsText.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackButton()
    {
        Debug.Log("BackButton");

        // Enable all main menu UI
        play.SetActive(true);
        credits.SetActive(true);
        exit.SetActive(true);
        menu.SetActive(true);

        // Disable all main menu UI
        creditsText.SetActive(false);
        backButton.SetActive(false);
    }

    // Exits the game
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
