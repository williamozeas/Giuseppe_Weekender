using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptedBurn : ScriptedEventAbstract
{
    public bool on;
}

public class ScriptedBurnTrigger : ScriptedAbstract<ScriptedBurn>
{
    ParticleSystem ps;

    public GameObject deathObj;

    protected override bool IsInFixedUpdate => true;

    protected override void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    protected override void TriggerEvent(ScriptedBurn burn)
    {
        if (burn.on)
        {
            BurnOn();
        }
        else
        {
            BurnOff();
        }
    }

    protected override void UnTriggerEvent(ScriptedBurn burn)
    {
        if (burn.on)
        {
            BurnOff();
        }
        else
        {
            BurnOn();
        }
    }

    protected void BurnOn()
    {
        ps.Play();
        deathObj.SetActive(true);
    }

    protected void BurnOff()
    {
        ps.Stop();
        deathObj.SetActive(false);
    }
}
