using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using UnityEngine.XR;


public class Player : MonoBehaviour
{
    [Header("Stats")] public float rewindRampIncrease = 0.005f;
    
    //private vars
    private float rewindValuePlayer;
    private float rewindValueWorld;
    private float rewindIntensity = 0.02f;
    private float rewindRampPlayer = 1;
    public float RewindRampPlayer => rewindRampPlayer;
    private float rewindRampWorld = 1;
    public float RewindRampWorld => rewindRampWorld;
    private bool isRewindingPlayer = false;
    public bool IsRewindingPlayer => isRewindingPlayer;
    private bool isRewindingWorld = false;
    public bool IsRewindingWorld => isRewindingWorld;

    private bool hasDied = false;
    private bool hasRunOutOfTime = false;
    
    //references
    private KCharacterController controller;
    private Collider[] colliders;
    private InstancedRewindManager playerRewindManager;
    public InstancedRewindManager PlayerRewinder => playerRewindManager;
    
    private RewindManager worldRewindManager;
    public RewindManager WorldRewindManager => worldRewindManager;

    private IGrabbable grabbedObject;

    private Watchable watchedObject;
    
    //Awake is called before Start
    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        controller = gameObject.GetComponent<KCharacterController>();
        colliders = GetComponentsInChildren<Collider>(); //may need to be more fine-grained if we have temp. colliders
        playerRewindManager = GetComponent<InstancedRewindManager>();
    }

    private void OnEnable()
    {
        GameManager.OnDie += OnDie;
        GameManager.OnRunOutOfTime += OnRunOutOfTime;
    }
    
    private void OnDisable()
    {
        GameManager.OnDie -= OnDie;
        GameManager.OnRunOutOfTime -= OnRunOutOfTime;
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

        HandleWatching();
    }

    void FixedUpdate()
    {
        HandleRewindInput();
    }

    private void HandleRewindInput()
    {
        //Rewind Self
        if(hasDied || hasRunOutOfTime || Input.GetButton("Rewind Self"))                     //Change keycode for your own custom key if you want
        {
            //While holding the button, we will gradually rewind more and more time into the past
            if (Input.GetButton("Rewind Self") && !hasRunOutOfTime)
            {
                rewindValuePlayer += rewindIntensity * Mathf.Pow(rewindRampPlayer, 2);
                rewindRampPlayer += rewindRampIncrease;
            }

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
                rewindRampPlayer = 1;
                isRewindingPlayer = false;
            }
        }
        
        //Rewind World
        if(hasRunOutOfTime || hasDied || Input.GetButton("Rewind World"))                     //Change keycode for your own custom key if you want
        {
            if (Input.GetButton("Rewind World") && !hasDied)
            { //While holding the button, we will gradually rewind more and more time into the past
                rewindValueWorld += rewindIntensity * Mathf.Pow(rewindRampWorld, 2);
                rewindRampWorld += rewindRampIncrease;
                
                if (rewindValueWorld < 0)
                {
                    rewindValueWorld = 0;
                    rewindRampWorld = 0;
                }
            }           

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
                rewindRampWorld = 1;
                isRewindingWorld = false;
            }
        }
    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        //Release from death state when stopping rewind after death
        if (hasDied && Input.GetButtonUp("Rewind Self"))
        {
            hasDied = false;
        }
        
        //Release from out of time state when stopping rewind after time runs out
        if (hasRunOutOfTime && Input.GetButtonUp("Rewind World"))
        {
            hasRunOutOfTime = false;
        }

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
            coll.enabled = true;
        }
    }

    private void OnDie()
    {
        hasDied = true; //triggers rewind code in HandleRewindInput
    }

    private void OnRunOutOfTime()
    {
        hasRunOutOfTime = true; //triggers rewind code in HandleRewindInput
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
                controller.Grab();
            } else {
                grabbedObject.Dropped();
                grabbedObject = null;
            }
        }
    }

    private void HandleWatching()
    {
        if (Input.GetKeyDown("h")) {
            //Grab pressed
            if (watchedObject == null) {
                var colliders = Physics.OverlapSphere(transform.position, 1.8f);
                foreach(var collider in colliders) {
                    if (collider.gameObject.TryGetComponent<Watchable>(out Watchable watchableObject )) {
                        watchableObject.Watched();
                        watchedObject = watchableObject;
                    }
                }
                controller.Grab();
            } else {
                watchedObject.Unwatched();
                watchedObject = null;
            }
        }
    }
}
