using KinematicCharacterController;
using UnityEngine;

public class PlayerRewind : InstancedGenericRewind
{
    private KinematicCharacterMotor motor;
    private KCharacterController controller;
    protected override void Awake()
    {
        base.Awake();
        motor = GetComponent<KinematicCharacterMotor>();
        controller = GetComponent<KCharacterController>();
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

