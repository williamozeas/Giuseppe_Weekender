using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptedGravity : ScriptedEventAbstract
{
    public bool on;
}

public class ScriptedGravityTrigger : ScriptedAbstract<ScriptedGravity>
{
    private Rigidbody rb;
    protected override bool IsInFixedUpdate => true;

    protected override void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    protected override void TriggerEvent(ScriptedGravity burn)
    {
        if (burn.on)
        {
            GravityOn();
        }
        else
        {
            GravityOff();
        }
    }

    protected override void UnTriggerEvent(ScriptedGravity burn)
    {
        if (burn.on)
        {
            GravityOff();
        }
        else
        {
            GravityOn();
        }
    }

    protected void GravityOn()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    protected void GravityOff()
    {
        rb.useGravity = false;
        rb.isKinematic = false;
    }
}
