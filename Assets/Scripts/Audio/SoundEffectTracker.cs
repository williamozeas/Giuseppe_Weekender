using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundEffectTracker : MonoBehaviour
{
    public Timeline Timeline = Timeline.World;
    private List<ReversibleSoundEffect> _sfx = new List<ReversibleSoundEffect>();
    private List<ReversibleSoundEffect> _playingSFX = new List<ReversibleSoundEffect>();

    private bool _justRewinded = false;

    private void OnEnable()
    {
        if (Timeline == Timeline.World)
        {
            RewindManager.StopRewind += OnStopRewind;
        }
    }

    private void OnDisable()
    {
        if (Timeline == Timeline.World)
        {
            RewindManager.StopRewind -= OnStopRewind;
        }
    }

    void FixedUpdate()
    //in FixedUpdate to match our FixedUpdate method of time tracking
    {
        //replaying sounds
        //TODO: currently this doesn't work with player time sounds because there is no "player timer"
        // ReversibleSoundEffect peek;
        if (((Timeline == Timeline.World && RewindManager.IsBeingRewinded) //world rewound
             || (Timeline == Timeline.Player && GameManager.Instance.Player.PlayerRewinder.IsBeingRewinded)) //or player rewound
            && _sfx.Count > _playingSFX.Count) //and we're at the correct time
        {
            ReversibleSoundEffect peek = _sfx[_sfx.Count - 1 - _playingSFX.Count];
            if (GameManager.Instance.Time < peek.times.Item2)
            {
                //TODO: right now if you start to reverse, then go forwards, then reverse again it will not play
                if (!_justRewinded)
                {
                    peek.OnReverse();
                }
                _playingSFX.Add(peek);
            }
        }

        for (int i = _playingSFX.Count - 1; i >= 0; i--)
        {
            ReversibleSoundEffect sfx = _playingSFX[i];
            if (sfx.times.Item2 < GameManager.Instance.Time)
            {
                _playingSFX.RemoveAt(i);
                continue;
            }

            if (sfx.times.Item1 > GameManager.Instance.Time)
            {
                _playingSFX.RemoveAt(i);
                _sfx.Remove(sfx);
                continue;
            }

            if (RewindManager.IsBeingRewinded)
            {
                sfx.SetSpeed(-GameManager.Instance.Player.RewindRampWorld);
            }
            else
            {
                sfx.SetSpeed(1);
            }
        }

        //_justRewinded is only true on the first frame you rewind, to stop already playing clips from resetting
        if (RewindManager.IsBeingRewinded)
        {
            _justRewinded = false;
        }
        else
        {
            _justRewinded = true;
        }
    }

    private void OnStopRewind()
    {
        // _reversedSfxToAdd.RemoveAll(sfx =>
        // {
        //     return sfx.times.Item1 > GameManager.Instance.Time;
        // });
        // _reversedSfxToAdd.Sort((a,b) => ReversibleSoundEffect.Compare(a,b,RewindManager.IsBeingRewinded));
        // _reversedSfxToAdd.ForEach(effect =>
        // {
        //     
        // });
    }

    public void AddSfx(ReversibleSoundEffect newSfx)
    {
        _sfx.Add(newSfx);
    }

    public void AddToPlaying(ReversibleSoundEffect newSfx)
    {
        _playingSFX.Add(newSfx);
    }
}
