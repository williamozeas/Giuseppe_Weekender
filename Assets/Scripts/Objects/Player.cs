using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using UnityEngine.XR;


public class Player : MonoBehaviour
{
    // [Header("Stats")] 
    
    //private vars
    private float rewindValue;
    private float rewindIntensity = 0.02f;
    private bool isRewinding = false;
    
    //references
    private KCharacterController controller;
    private InstancedRewindManager playerRewindManager;
    public InstancedRewindManager PlayerRewinder => playerRewindManager;
    
    private RewindManager worldRewindManager;
    public RewindManager WorldRewindManager => worldRewindManager;
    
    //Awake is called before Start
    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        controller = gameObject.GetComponent<KCharacterController>();
        playerRewindManager = GetComponent<InstancedRewindManager>();
        worldRewindManager = GameManager.Instance.RewindManager;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterInput();
    }

    void FixedUpdate()
    {
        HandleRewindInput();
    }

    private void HandleRewindInput()
    {
        if(Input.GetButton("Rewind Self"))                     //Change keycode for your own custom key if you want
        {
            rewindValue += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

            if (!isRewinding)
            {
                playerRewindManager.StartRewindTimeBySeconds(rewindValue);
            }
            else
            {
                if(playerRewindManager.HowManySecondsAvailableForRewind>rewindValue)      //Safety check so it is not grabbing values out of the bounds
                    playerRewindManager.SetTimeSecondsInRewind(rewindValue);
            }
            isRewinding = true;
        }
        else
        {
            if(isRewinding)
            {
                playerRewindManager.StopRewindTimeBySeconds();
                rewindValue = 0;
                isRewinding = false;
            }
        }
        
        //Rewind World
        if(Input.GetButton("Rewind World"))                     //Change keycode for your own custom key if you want
        {
            rewindValue += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

            if (!isRewinding)
            {
                worldRewindManager.StartRewindTimeBySeconds(rewindValue);
            }
            else
            {
                if(worldRewindManager.HowManySecondsAvailableForRewind>rewindValue)      //Safety check so it is not grabbing values out of the bounds
                    worldRewindManager.SetTimeSecondsInRewind(rewindValue);
            }
            isRewinding = true;
        }
        else
        {
            if(isRewinding)
            {
                worldRewindManager.StopRewindTimeBySeconds();
                rewindValue = 0;
                isRewinding = false;
            }
        }
    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        characterInputs.MoveAxisRight = Input.GetAxisRaw("Horizontal");
        characterInputs.JumpDown = Input.GetButtonDown("Jump");
        // characterInputs.CrouchDown = Input.GetButtonDown("Crouch");
        // characterInputs.CrouchUp = Input.GetButtonUp("Crouch");

        // Apply inputs to character
        controller.SetInputs(ref characterInputs);
    }
}
