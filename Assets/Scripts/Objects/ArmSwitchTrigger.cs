using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSwitchTrigger : MonoBehaviour
{
    private Boss boss;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SwitchToArm();
        }
    }
}
