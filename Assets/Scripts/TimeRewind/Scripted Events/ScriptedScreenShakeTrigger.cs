using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[System.Serializable]
public class ScriptedScreenShake : ScriptedEventAbstract
{
    public float intensity;
    public float duration;
}

public class ScriptedScreenShakeTrigger : ScriptedAbstract<ScriptedScreenShake>
{
    private CinemachineShake shaker;
    protected override void Awake()
    {
        shaker = GetComponent<CinemachineShake>();
    }

    protected override void TriggerEvent(ScriptedScreenShake shake)
    {
        shaker.ShakeCamera(shake.intensity, shake.duration);
    }

    protected override void UnTriggerEvent(ScriptedScreenShake shake)
    {
        //
    }
}
