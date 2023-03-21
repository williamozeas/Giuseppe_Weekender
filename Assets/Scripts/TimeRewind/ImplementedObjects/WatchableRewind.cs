using UnityEngine;
using System.Collections.Generic;
using System;


public class WatchableRewind : RewindAbstract
{

    [SerializeField] bool trackPositionRotation;
    [SerializeField] bool trackVelocity;
    [SerializeField] bool trackAnimator;
    [SerializeField] bool trackAudio;
    [SerializeField] bool trackParticles;

    public bool watched;

    [Tooltip("Fill particle settings only if you check Track Particles")]
    [SerializeField] ParticlesSetting particleSettings;

    protected override void Rewind(float seconds)
    {
        if (!watched){
            if (trackPositionRotation)
                RestorePositionAndRotation(seconds);
            if (trackVelocity)
                RestoreVelocity(seconds);
            if (trackAnimator)
                RestoreAnimator(seconds);
            if (trackParticles)
                RestoreParticles(seconds);
            if(trackAudio)
                RestoreAudio(seconds);
        }
    }

    protected override void Track()
    {
        if (!watched){
            if (trackPositionRotation)
                TrackPositionAndRotation();
            if (trackVelocity)
                TrackVelocity();
            if (trackAnimator)
                TrackAnimator();
            if (trackParticles)
                TrackParticles();
            if (trackAudio)
                TrackAudio();
        } else {
            if (trackPositionRotation)
                TrackGrabbedPositionAndRotation();
            if (trackVelocity)
                TrackGrabbedVelocity();
            if (trackAnimator)
                TrackGrabbedAnimator();
            if (trackParticles)
                TrackGrabbedParticles();
            if (trackAudio)
                TrackGrabbedAudio();
        }

    }
    private void Start()
    {
        InitializeParticles(particleSettings);
        watched = false;
    }

    public void Watched()
    {
        watched = true;
    }

    public void Unwatched()
    {
        watched = false;
    }

}

