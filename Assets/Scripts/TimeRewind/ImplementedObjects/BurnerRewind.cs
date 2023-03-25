using UnityEngine;


//This script is showing setup of defaulty implemented tracking solutions (eg. tracking particles, audio...) in combination with custom variable tracking.
public class BurnerRewind : RewindAbstract
{
    CircularBuffer<bool> trackedRewind;     //For storing data, use this CircularBuffer class
    private ScriptedBurnTrigger burner;
    private void Start()
    {
        burner = GetComponent<ScriptedBurnTrigger>();
        trackedRewind = new CircularBuffer<bool>();  //Circular buffer must be initialized in start method, it cannot use field initialization
        
    }


    //In this method define what will be tracked. In our case we want to track already implemented audio tracking,particle tracking + new custom timer tracking
    protected override void Track()
    {
        TrackBurner();
    }

    //In this method define, what will be restored on time rewinding. In our case we want to restore Particles, Audio and custom implemented Timer
    protected override void Rewind(float seconds)
    {
        RestoreBurner(seconds);
    }


    // This is an example of custom variable tracking
    public void TrackBurner()
    {
        trackedRewind.WriteLastValue(burner.On);
    }


    // This is an example of custom variable restoring
    public void RestoreBurner(float seconds)
    {
        bool rewindValue= trackedRewind.ReadFromBuffer(seconds);
        burner.SetBurner(rewindValue);
    }
}
