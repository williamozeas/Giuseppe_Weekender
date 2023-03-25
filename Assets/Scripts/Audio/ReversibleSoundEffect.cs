using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public enum Timeline
{
    World,
    Player
}

//Function 
public class ReversibleSoundEffect
{
    public (float, float) times;
    private Action _sfxAction;
    private InstancedRewindManager _manager = null;
    private AudioSource _source;
    public AudioSource Source => _source;
    private Timeline _timeline;
    private float _length;
    
    public ReversibleSoundEffect(Action sfxAction, AudioSource source, Timeline timeline, float length = -1f)
    {
        _sfxAction = sfxAction;
        _source = source;
        _timeline = timeline;
        if (length < 0)
        {
            if (source.loop)
            {
                _length = 31f;
            }
            _length = source.clip.length;
        }
        
    }

    public void Play()
    {
        times = (GameManager.Instance.Time, GameManager.Instance.Time + _length);
        _source.pitch = 1;
        _source.timeSamples = 0;
        _sfxAction();
        switch (_timeline)
        {
            case(Timeline.World):
            {
                AudioManager.Instance.WorldSfxTracker.AddSfx(this);
                AudioManager.Instance.WorldSfxTracker.AddToPlaying(this);
                break;
            }
            case(Timeline.Player):
            {
                AudioManager.Instance.PlayerSfxTracker.AddSfx(this);
                AudioManager.Instance.PlayerSfxTracker.AddToPlaying(this);
                break;
            }
        }
    }

    public void OnReverse()
    {
        _source.timeSamples = _source.clip.samples - 1;
        switch (_timeline)
        {
            case(Timeline.World):
            {
                AudioManager.Instance.WorldSfxTracker.AddToPlaying(this);
                _source.pitch = GameManager.Instance.Player.RewindRampWorld;
                break;
            }
            case(Timeline.Player):
            {
                AudioManager.Instance.PlayerSfxTracker.AddToPlaying(this);
                _source.pitch = GameManager.Instance.Player.RewindRampPlayer;
                break;
            }
        }
        _sfxAction();
    }

    public void SetSpeed(float speed)
    {
        _source.pitch = speed;
    }

    public void Stop()
    {
        _source.Stop();
        times.Item2 = GameManager.Instance.Time;
    }

    public void SetTimeSamples(int samples)
    {
        _source.timeSamples = samples;
    }

    public static int Compare(ReversibleSoundEffect a, ReversibleSoundEffect b, bool isReversing)
    {
        if (isReversing)
        {
            return -a.times.Item2.CompareTo(b.times.Item2);
        }
        return a.times.Item1.CompareTo(b.times.Item1);
    }
}
