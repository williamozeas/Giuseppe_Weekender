using UnityEngine;
using System.Collections.Generic;
using System;


public class GrabbableRewind : RewindAbstract
{

    public class TimeTuple
    {
        public float startTime;
        public float endTime;

        public TimeTuple(float sT, float eT) {
            startTime = sT;
            endTime = eT;
        }

        public override string ToString()
        {
            return "(" + startTime + ", " + endTime + ")";
        }
    }

    [SerializeField] bool trackPositionRotation;
    [SerializeField] bool trackVelocity;
    [SerializeField] bool trackAnimator;
    [SerializeField] bool trackAudio;
    [SerializeField] bool trackParticles;

    public bool grabbed;

    Vector3 grabbedTravelOffset;
    Vector3 posBeforeLatestGrab;

    [Tooltip("Fill particle settings only if you check Track Particles")]
    [SerializeField] ParticlesSetting particleSettings;

    protected override void Rewind(float seconds)
    {
        if (!grabbed){
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
        if (!grabbed){
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
        grabbed = false;
    }

    public void Grabbed(Vector3 preGrabPos)
    {
        grabbed = true;
        posBeforeLatestGrab = preGrabPos;
    }

    public void Dropped(Vector3 postGrabPos)
    {
        grabbed = false;
        grabbedTravelOffset = postGrabPos - posBeforeLatestGrab;
        OffsetPositionAndRotation(grabbedTravelOffset);
    }

}

