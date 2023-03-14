using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathButtonPrompt : MonoBehaviour
{
    public Image RewindPlayerPrompt;

    // Start is called before the first frame update
    void Start()
    {
        RewindPlayerPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        GameManager.OnDie += OnDie;
    }
    
    private void OnDisable()
    {
        GameManager.OnDie -= OnDie;
    }

    private void OnDie()
    {
        //Activate canvas/elements here
        RewindPlayerPrompt.gameObject.SetActive(true);
    }
}
