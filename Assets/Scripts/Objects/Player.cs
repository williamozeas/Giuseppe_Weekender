using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using UnityEngine.XR;


public class Player : MonoBehaviour
{
    [Header("Stats")]
    
    private KCharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    //Awake is called before Start
    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<KCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterInput();
    }
    
    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        characterInputs.MoveAxisRight = Input.GetAxisRaw("Horizontal");
        characterInputs.JumpDown = Input.GetButtonDown("Jump");
        // characterInputs.CrouchDown = Input.GetButtonDown("Crouch");
        // characterInputs.CrouchUp = Input.GetButtonUp("Crouch");
        
        // PlayerCharacterInputs temp = new PlayerCharacterInputs();
        // if (!temp.Equals(characterInputs))
        // {
        //     Debug.Log("oh!");
        // }

        // Apply inputs to character
        controller.SetInputs(ref characterInputs);
    }
}
