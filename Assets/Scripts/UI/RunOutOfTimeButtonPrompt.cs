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
    }
    
    private void OnDisable()
    {
        GameManager.OnRunOutOfTime -= OnRunOutOfTime;
    }

    private void OnRunOutOfTime()
    {
        //Activate canvas/elements here
        RewindTimePrompt.gameObject.SetActive(true);
    }
}
