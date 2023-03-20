using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class ButtonInteractions : MonoBehaviour
{
    public GameObject MainBackground;
    public GameObject LandingPage;
    public GameObject HowToPlayPage;
    public GameObject PlayPage;
    // public GameObject EndingPage;

    public void clickStartFromLanding() {
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
        PlayPage.gameObject.SetActive(true);
        // EndingPage.gameObject.SetActive(false);
        MainBackground.gameObject.SetActive(false);
    }

    public void clickHowToPlayFromLanding() {
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(true);
        PlayPage.gameObject.SetActive(false);
        // EndingPage.gameObject.SetActive(false);
    }

    public void clickStartFromHowToPlay() {
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
        PlayPage.gameObject.SetActive(true);
        // EndingPage.gameObject.SetActive(false);
        MainBackground.gameObject.SetActive(false);
    }

    public void clickBackFromHowToPlay() {
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
        PlayPage.gameObject.SetActive(false);
        // EndingPage.gameObject.SetActive(false);
        MainBackground.gameObject.SetActive(true);
    }

    public void clickExitFromEnd() {
        // GameManager.Instance.GameState = GameState.Menu;
        
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
        PlayPage.gameObject.SetActive(false);
        // EndingPage.gameObject.SetActive(false);
        MainBackground.gameObject.SetActive(true);
    }

    public void clickPlayAgainFromEnd() {
        // GameManager.Instance.GameState = GameState.Menu;
        // GameManager.Instance.GameState = GameState.Playing;
        
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
        PlayPage.gameObject.SetActive(true);
        // EndingPage.gameObject.SetActive(false);
        MainBackground.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        MainBackground.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
