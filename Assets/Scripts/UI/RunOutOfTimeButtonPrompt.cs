using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOutOfTimeButtonPrompt : MonoBehaviour
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
        GameManager.OnRunOutOfTime += OnRunOutOfTime;
    }
    
    private void OnDisable()
    {
        GameManager.OnRunOutOfTime -= OnRunOutOfTime;
    }

    private void OnRunOutOfTime()
    {
        //Activate canvas/elements here
    }
}
