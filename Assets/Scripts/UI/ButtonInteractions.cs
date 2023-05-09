using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class ButtonInteractions : MonoBehaviour
{
    public GameObject Background;
    public GameObject LandingPage;
    public GameObject HowToPlayPage;
    public GameObject StoryPage;
    public GameObject EndingPage;

    public void startGame() { // from landing page to story
        Background.gameObject.SetActive(false);
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
        StoryPage.gameObject.SetActive(true);
        EndingPage.gameObject.SetActive(false);
    }

    public void playGame() { // from story to gameplay
        GameManager.Instance.CurrentScene = SceneNum.Kitchen;
    }

    public void backFromStory() {
        Background.gameObject.SetActive(true);
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
        StoryPage.gameObject.SetActive(false);
        EndingPage.gameObject.SetActive(false);
    }

    public void seeHowToPlay() { // from landing to htp
        Background.gameObject.SetActive(true);
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(true);
        StoryPage.gameObject.SetActive(false);
        EndingPage.gameObject.SetActive(false);
    }

    public void startFromHtp() {
        Background.gameObject.SetActive(false);
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
        StoryPage.gameObject.SetActive(true);
        EndingPage.gameObject.SetActive(false);
    }

    public void backFromHtp() {
        Background.gameObject.SetActive(true);
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
        StoryPage.gameObject.SetActive(false);
        EndingPage.gameObject.SetActive(false);
    }

    public void playAgain() {
        GameManager.Instance.CurrentScene = SceneNum.Kitchen;
    }

    public void exitGame() {
        GameManager.Instance.GameState = GameState.Menu;
        Background.gameObject.SetActive(true);
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
        StoryPage.gameObject.SetActive(false);
        EndingPage.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
