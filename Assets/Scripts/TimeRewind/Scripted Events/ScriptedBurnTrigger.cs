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
    private AudioSource audioSource;
    private ReversibleSoundEffect sfx;

    public GameObject deathObj;

    public bool On;

    protected override bool IsInFixedUpdate => true;

    protected override void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInChildren<AudioSource>();
        On = false;
    }

    protected override void TriggerEvent(ScriptedBurn burn)
    {
        SetBurner(burn.on);
    }

    protected override void UnTriggerEvent(ScriptedBurn burn)
    {
        SetBurner(!burn.on);
    }

    public void SetBurner(bool on)
    {
        if (on)
        {
            BurnOn();
        }
        else
        {
            BurnOff();
        }
    }

    protected void BurnOn()
    {
        ps.gameObject.SetActive(true);
        ps.Play();
        deathObj.SetActive(true);
        sfx = new ReversibleSoundEffect(() => audioSource.Play(), audioSource, Timeline.World);
        sfx.Play();
        On = true;
    }

    protected void BurnOff()
    {
        ps.Stop();
        ps.gameObject.SetActive(false);
        deathObj.SetActive(false);
        audioSource.Stop();
        if(sfx != null) {
            sfx.Stop();
        }
        On = false;
    }
}
