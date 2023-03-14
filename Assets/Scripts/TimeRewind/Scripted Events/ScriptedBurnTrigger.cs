using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptedBurn : ScriptedEventAbstract
{
}

public class ScriptedBurnTrigger : ScriptedAbstract<ScriptedBurn>
{
    ParticleSystem ps;

    protected override bool IsInFixedUpdate => true;

    protected override void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    protected override void TriggerEvent(ScriptedBurn burn)
    {
        ps.Play();
    }

    protected override void UnTriggerEvent(ScriptedBurn burn)
    {
        ps.Stop();
    }
}
