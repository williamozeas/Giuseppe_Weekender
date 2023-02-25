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
    private float rewindIntensity;
    private bool isRewinding = false;
    
    //references
    private KCharacterController controller;
    private PlayerRewind rewinder;
    
    //Awake is called before Start
    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        controller = gameObject.GetComponent<KCharacterController>();
        rewinder = GetComponent<PlayerRewind>();
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
                rewinder.RewindTo(rewindValue);
            }
            else
            {
                // if(rewindManager.HowManySecondsAvailableForRewind>rewindValue)      //Safety check so it is not grabbing values out of the bounds
                //     rewindManager.SetTimeSecondsInRewind(rewindValue);
            }
            isRewinding = true;
        }
        else
        {
            if(isRewinding)
            {
                // rewindManager.StopRewindTimeBySeconds();
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
