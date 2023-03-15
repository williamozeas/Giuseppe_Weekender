using KinematicCharacterController;
using UnityEngine;

public class PlayerRewind : InstancedGenericRewind
{
    [SerializeField] private Animator animatorRef;
    private KinematicCharacterMotor motor;
    private Player player;
    protected override void Awake()
    {
        base.Awake();
        motor = GetComponent<KinematicCharacterMotor>();
        animator = animatorRef;
        if (animator != null)
            for (int i = 0; i < animator.layerCount; i++)
                trackedAnimationTimes.Add(new CircularBuffer<AnimationValues>(rewindManager));
    }
    
    protected override void RestorePositionAndRotation(float seconds)
    {
        PositionAndRotationValues valuesToRead = trackedPositionsAndRotation.ReadFromBuffer(seconds);
        // transform.SetPositionAndRotation(valuesToRead.position, valuesToRead.rotation);
        motor.SetPositionAndRotation(valuesToRead.position, valuesToRead.rotation);
    }
    
    protected override void TrackVelocity()
    {
        trackedVelocities.WriteLastValue(motor.BaseVelocity);
    }
    
    protected override void RestoreVelocity(float seconds)
    {
        motor.BaseVelocity = trackedVelocities.ReadFromBuffer(seconds);
    }

}

