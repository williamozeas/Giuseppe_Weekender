using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunOutOfTimeButtonPrompt : MonoBehaviour
{
    public Image RewindTimePrompt;
    private Coroutine waitingCoroutine;

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
        //Activate canvas/elements 
        RewindTimePrompt.gameObject.SetActive(true);
        if(waitingCoroutine != null) StopCoroutine(waitingCoroutine);
        waitingCoroutine = StartCoroutine(WaitForRewind());
    }

    private void OnStartRewind()
    {
        //Deactivate canvas/elements here
        RewindTimePrompt.gameObject.SetActive(false);
    }

    private IEnumerator WaitForRewind()
    {
        while (true)
        {
            if (Input.GetButtonDown("Rewind World"))
            {
                break;
            }
            yield return null;
        }
        OnStartRewind();
    }
}
