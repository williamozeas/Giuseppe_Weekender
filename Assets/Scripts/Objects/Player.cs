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
    public bool IsRewindingPlayer => isRewindingPlayer;
    private bool isRewindingWorld = false;
    public bool IsRewindingWorld => isRewindingWorld;
    
    //references
    private KCharacterController controller;
    private Collider[] colliders;
    private InstancedRewindManager playerRewindManager;
    public InstancedRewindManager PlayerRewinder => playerRewindManager;
    
    private RewindManager worldRewindManager;
    public RewindManager WorldRewindManager => worldRewindManager;

    private IGrabbable grabbedObject;
    
    //Awake is called before Start
    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        controller = gameObject.GetComponent<KCharacterController>();
        colliders = GetComponentsInChildren<Collider>(); //may need to be more fine-grained if we have temp. colliders
        playerRewindManager = GetComponent<InstancedRewindManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        worldRewindManager = GameManager.Instance.RewindManager;
        grabbedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterInput();

        HandleGrabbing();
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
                DeactivateColliders();
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
                ActivateColliders();
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

    public void DeactivateColliders()
    {
        foreach (Collider coll in colliders)
        {
            coll.enabled = false;
        }
    }
    
    public void ActivateColliders()
    {
        foreach (Collider coll in colliders)
        {
            coll.enabled = false;
        }
    }

    private void HandleGrabbing()
    {
        if (Input.GetKeyDown("g")) {
            //Grab pressed
            if (grabbedObject == null) {
                var colliders = Physics.OverlapSphere(transform.position, 1.8f);
                foreach(var collider in colliders) {
                    if (collider.gameObject.TryGetComponent(out IGrabbable grabbableObject)) {
                        grabbableObject.Grabbed(gameObject);
                        grabbedObject = grabbableObject;
                    }
                }
            } else {
                grabbedObject.Dropped();
                grabbedObject = null;
            }
        }
    }
}
