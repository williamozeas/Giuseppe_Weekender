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
        GameManager.Instance.Player.PlayerRewinder.StartRewind += OnStartRewind;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        GameManager.OnDie += OnDie;
        GameManager.Instance.Player.PlayerRewinder.StartRewind += OnStartRewind;
    }
    
    private void OnDisable()
    {
        GameManager.OnDie -= OnDie;
        if(GameManager.Instance.Player && GameManager.Instance.Player.PlayerRewinder)
        GameManager.Instance.Player.PlayerRewinder.StartRewind -= OnStartRewind;
    }

    private void OnDie()
    {
        //Activate canvas/elements here
        RewindPlayerPrompt.gameObject.SetActive(true);
    }

    private void OnStartRewind()
    {
        //Deactivate canvas/elements here
        RewindPlayerPrompt.gameObject.SetActive(false);
    }
}
