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

    public void startGame() { // start from landing page
        SceneManager.LoadScene("Kitchen"); 
        Background.gameObject.SetActive(false);
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
    }

    public void seeHowToPlay() {
        Background.gameObject.SetActive(true);
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(true);
    }

    public void clickStartFromInstructions() {
        SceneManager.LoadScene("Kitchen"); 
        Background.gameObject.SetActive(false);
        LandingPage.gameObject.SetActive(false);
        HowToPlayPage.gameObject.SetActive(false);
    }

    public void clickBackFromInstructions() {
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Background.gameObject.SetActive(true);
        LandingPage.gameObject.SetActive(true);
        HowToPlayPage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}