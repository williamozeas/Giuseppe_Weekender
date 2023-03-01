using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class ScriptedForce : ScriptedEventAbstract
{
    public bool isTorque;
    public Vector3 force;
}

public class ScriptedForces<T> : ScriptedAbstract<T> where T : ScriptedForce
{
    private Rigidbody rb;
    protected override bool IsInFixedUpdate => true;

    // Start is called before the first frame update
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void TriggerEvent(T force)
    {
        if (!force.isTorque)
        {
            rb.AddForce(force.force, ForceMode.Impulse);
        }
        else
        {
            rb.AddTorque(force.force, ForceMode.Impulse);
        }

    }
}
