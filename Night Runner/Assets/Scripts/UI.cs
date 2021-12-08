using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UI : MonoBehaviour
{
    // Establish game objects
    public GameObject play = null;
    public GameObject credits = null;
    public GameObject exit = null;
    public GameObject menu = null;
    public GameObject creditsPrevious = null;
    public GameObject creditsNext = null;
    public GameObject canvas = null;
    public GameObject creditsParent = null;
    public GameObject tutorialButton = null;
    public GameObject tutorial = null;

    public List<GameObject> creditsPages = new List<GameObject>();
    int currentCreditsPage = 0;

    void Start()
    {
		Time.timeScale = 0.0f; // pause

        // Sets the active state of each of all UI elements
        play.SetActive(true);
        credits.SetActive(true);
        creditsParent.SetActive(false);
        exit.SetActive(true);
        menu.SetActive(true);
        tutorialButton.SetActive(true);
        tutorial.SetActive(false);
    }

    // Plays the game
    public void Play()
    {
        Debug.Log("Play");

		canvas.SetActive(false);

		Time.timeScale = 1.0f; // game speed or un-pause
    }

    // Opens the credits menu
    public void CreditsText()
    {
        Debug.Log("Credits");

        // Disable all main menu UI
        play.SetActive(false);
        credits.SetActive(false);
        creditsParent.SetActive(true);
        exit.SetActive(false);
        menu.SetActive(false);
        tutorialButton.SetActive(false);
        tutorial.SetActive(false);

        CreditsPage(0);

        // Enable all credits menu UI
        creditsNext.SetActive(true);
        creditsPrevious.SetActive(true);
    }

    public void ShowTutorial() {
        play.SetActive(false);
        credits.SetActive(false);
        creditsParent.SetActive(false);
        exit.SetActive(false);
        menu.SetActive(false);
        tutorialButton.SetActive(false);
        tutorial.SetActive(true);
    }

    public void OnPrevCreditsClick() {
    	currentCreditsPage--;
    	currentCreditsPage = currentCreditsPage < 0 ? creditsPages.Count - 1 : currentCreditsPage;
    	CreditsPage(currentCreditsPage);
    }

	public void OnNextCreditsClick() {
    	currentCreditsPage++;
    	currentCreditsPage = currentCreditsPage > creditsPages.Count - 1 ? 0 : currentCreditsPage;
    	CreditsPage(currentCreditsPage);
    }

    public void CreditsPage(int page) {
    	foreach (var p in creditsPages) {
			p.SetActive(false);
    	}

    	creditsPages[page].SetActive(true);
    }

    public void BackButton()
    {
        Debug.Log("BackButton");

        // Enable all main menu UI
        play.SetActive(true);
        credits.SetActive(true);
        creditsParent.SetActive(false);
        exit.SetActive(true);
        menu.SetActive(true);
        tutorialButton.SetActive(true);
        tutorial.SetActive(false);
    }

    // Exits the game
    public void Exit()
    {
        Debug.Log("Exit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
