using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathButtonPrompt : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
