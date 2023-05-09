using System;
using UnityEngine;

public class GenericRewind : RewindAbstract
{
    [SerializeField] bool trackPositionRotation;
    [SerializeField] bool trackVelocity;
    [SerializeField] bool trackAnimator;
    [SerializeField] bool trackAudio;
    [SerializeField] bool trackParticles;

    [Tooltip("Fill particle settings only if you check Track Particles")]
    [SerializeField] ParticlesSetting particleSettings;

    private float _seconds;
    private float _lastUpdated;
    
    protected override void Rewind(float seconds)
    {
        _seconds = seconds;
        if (trackPositionRotation)
        {
            if (trackVelocity)
            {
                RestorePositionAndRotationRigidbody(seconds);
            }
            else
            {
                RestorePositionAndRotation(seconds);
            }
        }

        if (trackVelocity)
            RestoreVelocity(seconds);
        if (trackAnimator)
            RestoreAnimator(seconds);
        // if (trackParticles)
        //     RestoreParticles(seconds);
        if(trackAudio)
            RestoreAudio(seconds);
    }

    private void Update()
    {
        if (RewindManager.IsBeingRewinded && trackParticles &&  _seconds > _lastUpdated)
        {
            _lastUpdated = _seconds;
            RestoreParticles(_seconds);
        }
    }

    protected override void Track()
    {
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
    }
    private void Start()
    {
        InitializeParticles(particleSettings);
        Track();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        RewindManager.StopRewind += OnStopRewind;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        RewindManager.StopRewind -= OnStopRewind;
    }

    private void OnStopRewind()
    {
        if (trackVelocity)
        {
            body.velocity *= -1;
            body.angularVelocity *= -1;
        }
        
        //prevent particles from updating in the wrong time
        _seconds = -1;
        _lastUpdated = 0;
    }

}

