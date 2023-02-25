using UnityEngine;

public class PlayerRewindOld : RewindAbstract
{
    [SerializeField] bool trackPositionRotation;
    [SerializeField] bool trackVelocity;
    [SerializeField] bool trackAnimator;
    [SerializeField] bool trackAudio;
    [SerializeField] bool trackParticles;

    [Tooltip("Fill particle settings only if you check Track Particles")]
    [SerializeField] ParticlesSetting particleSettings;
    
    public float HowManySecondsAvailableForRewind { get; private set; }
    /// <summary>
    /// Variable defining how much into the past should be tracked, after set limit is hit, old values will be overwritten in circular buffer
    /// </summary>
    public static readonly float howManySecondsToTrack = 5;

    private float rewindSeconds = 0;
    
    //undo event subscribes to be on different timeline
    protected override void OnEnable()
    {
        HowManySecondsAvailableForRewind = 0;
    }
    protected override void OnDisable() { /* */ }
    
    protected override void Rewind(float seconds)
    {

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
        
        rewindSeconds = seconds;
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
    }

    public void RewindTo(float seconds)
    {
        Rewind(seconds);
    }

}

