using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunOutOfTimeButtonPrompt : MonoBehaviour
{
    public Image RewindTimePrompt;

    // Start is called before the first frame update
    void Start()
    {
        RewindTimePrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        GameManager.OnRunOutOfTime += OnRunOutOfTime;
        RewindManager.StartRewind += OnStartRewind;
    }
    
    private void OnDisable()
    {
        GameManager.OnRunOutOfTime -= OnRunOutOfTime;
        RewindManager.StartRewind -= OnStartRewind;
    }

    private void OnRunOutOfTime()
    {
        //Activate canvas/elements 
        RewindTimePrompt.gameObject.SetActive(true);
    }

    private void OnStartRewind()
    {
        //Deactivate canvas/elements here
        RewindTimePrompt.gameObject.SetActive(false);
    }
}
