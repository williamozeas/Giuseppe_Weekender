using UnityEngine;


//This script is showing setup of defaulty implemented tracking solutions (eg. tracking particles, audio...) in combination with custom variable tracking.
public class WorldTimer : RewindAbstract
{
    CircularBuffer<float> trackedTime;     //For storing data, use this CircularBuffer class
    private float time = 0;
    public float Time => time;

    private void Start()
    {
        trackedTime = new CircularBuffer<float>();  //Circular buffer must be initialized in start method, it cannot use field initialization
    }


    //In this method define what will be tracked. In our case we want to track already implemented audio tracking,particle tracking + new custom timer tracking
    protected override void Track()
    {
        time += UnityEngine.Time.fixedDeltaTime;
        TrackTimer();
    }

    //In this method define, what will be restored on time rewinding. In our case we want to restore custom implemented Timer
    protected override void Rewind(float seconds)
    {
        RestoreTimer(seconds);
    }

    // This is an example of custom variable tracking
    public void TrackTimer()
    {
        trackedTime.WriteLastValue(time);
    }

    // This is an example of custom variable restoring
    public void RestoreTimer(float seconds)
    {
        time = trackedTime.ReadFromBuffer(seconds);
    }
    
}
