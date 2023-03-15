using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathButtonPrompt : MonoBehaviour
{
    public Image RewindPlayerPrompt;
    private Coroutine waitingCoroutine;

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
        if(waitingCoroutine != null) StopCoroutine(waitingCoroutine);
        waitingCoroutine = StartCoroutine(WaitForRewind());
    }

    private void OnStartRewind()
    {
        //Deactivate canvas/elements here
        RewindPlayerPrompt.gameObject.SetActive(false);
    }

    private IEnumerator WaitForRewind()
    {
        while (true)
        {
            if (Input.GetButtonDown("Rewind Self"))
            {
                break;
            }
            yield return null;
        }
        OnStartRewind();
    }
}
