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
    private float rewindValuePlayer;
    private float rewindValueWorld;
    private float rewindIntensity = 0.02f;
    private bool isRewindingPlayer = false;
    private bool isRewindingWorld = false;
    
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
            rewindValuePlayer += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

            if (!isRewindingPlayer)
            {
                playerRewindManager.StartRewindTimeBySeconds(rewindValuePlayer);
            }
            else
            {
                if(playerRewindManager.HowManySecondsAvailableForRewind>rewindValuePlayer)      //Safety check so it is not grabbing values out of the bounds
                    playerRewindManager.SetTimeSecondsInRewind(rewindValuePlayer);
            }
            isRewindingPlayer = true;
        }
        else
        {
            if(isRewindingPlayer)
            {
                playerRewindManager.StopRewindTimeBySeconds();
                rewindValuePlayer = 0;
                isRewindingPlayer = false;
            }
        }
        
        //Rewind World
        if(Input.GetButton("Rewind World"))                     //Change keycode for your own custom key if you want
        {
            rewindValueWorld += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

            if (!isRewindingWorld)
            {
                worldRewindManager.StartRewindTimeBySeconds(rewindValueWorld);
            }
            else
            {
                if(worldRewindManager.HowManySecondsAvailableForRewind>rewindValueWorld)      //Safety check so it is not grabbing values out of the bounds
                    worldRewindManager.SetTimeSecondsInRewind(rewindValueWorld);
            }
            isRewindingWorld = true;
        }
        else
        {
            if(isRewindingWorld)
            {
                worldRewindManager.StopRewindTimeBySeconds();
                rewindValueWorld = 0;
                isRewindingWorld = false;
            }
        }
    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();


        if (!isRewindingPlayer)
        {
            // Build the CharacterInputs struct
            characterInputs.MoveAxisRight = Input.GetAxisRaw("Horizontal");
            characterInputs.JumpDown = Input.GetButtonDown("Jump");
            // characterInputs.CrouchDown = Input.GetButtonDown("Crouch");
            // characterInputs.CrouchUp = Input.GetButtonUp("Crouch");

            // Apply inputs to character
            controller.SetInputs(ref characterInputs);
        }
    }
}
