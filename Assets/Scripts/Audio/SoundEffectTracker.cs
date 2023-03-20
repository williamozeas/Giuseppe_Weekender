using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTracker : MonoBehaviour
{
    public Timeline Timeline = Timeline.World;
    private Stack<ReversibleSoundEffect> _sfx = new Stack<ReversibleSoundEffect>();
    private List<ReversibleSoundEffect> _playingSFX = new List<ReversibleSoundEffect>();

    private bool _justRewinded = false;
    
    void FixedUpdate()
    //in FixedUpdate to match our FixedUpdate method of time tracking
    {
        //replaying sounds
        //TODO: currently this doesn't work with player time sounds because there is no "player timer"
        ReversibleSoundEffect peek;
        if (((Timeline == Timeline.World && RewindManager.IsBeingRewinded) //world rewound
             || (Timeline == Timeline.Player && GameManager.Instance.Player.PlayerRewinder.IsBeingRewinded)) //or player rewound
            && _sfx.TryPeek(out peek) && GameManager.Instance.Time < peek.times.Item2) //and we're at the correct time
        {
            //TODO: right now if you start sfx, reverse it, let it fully play forwards again, then reverse, it will not play
            ReversibleSoundEffect pop = _sfx.Pop();
            if (!_justRewinded)
            {
                pop.OnReverse();
            }
            _playingSFX.Add(pop);
        }

        for (int i = _playingSFX.Count - 1; i >= 0; i--)
        {
            ReversibleSoundEffect sfx = _playingSFX[i];
            if (sfx.times.Item1 > GameManager.Instance.Time || sfx.times.Item2 < GameManager.Instance.Time)
            {
                _playingSFX.RemoveAt(i);
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

    public void AddSfx(ReversibleSoundEffect newSfx)
    {
        _sfx.Push(newSfx);
    }

    public void AddToPlaying(ReversibleSoundEffect newSfx)
    {
        _playingSFX.Add(newSfx);
    }
}
