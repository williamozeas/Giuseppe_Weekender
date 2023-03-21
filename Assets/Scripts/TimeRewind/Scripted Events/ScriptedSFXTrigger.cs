using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptedSFX : ScriptedEventAbstract
{
    public AudioClip clip;
    public AudioSource source;
    public Timeline timeline;
}

public class ScriptedSFXTrigger : ScriptedAbstract<ScriptedSFX>
{
    protected override bool IsInFixedUpdate => true;

    // Start is called before the first frame update
    protected override void Awake()
    {
        
    }

    protected override void TriggerEvent(ScriptedSFX scriptedSfx)
    {
        ReversibleSoundEffect sfx;
        if (scriptedSfx.clip != null)
        {
            scriptedSfx.source.clip = scriptedSfx.clip;
        }
        sfx = new ReversibleSoundEffect(() => scriptedSfx.source.Play(), scriptedSfx.source, scriptedSfx.timeline);
        sfx.Play();
    }
    
    protected override void UnTriggerEvent(ScriptedSFX force)
    {
    }
}
