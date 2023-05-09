using UnityEngine;


//This script is showing setup of defaulty implemented tracking solutions (eg. tracking particles, audio...) in combination with custom variable tracking.
public class SeesawRewind : RewindAbstract
{
    CircularBuffer<float> trackedRot;     //For storing data, use this CircularBuffer class
    //Button buttonScript;
    PlankSeeSaw seesawScript;

    //bool buttonPressed;
    float currRot;

    private void Start()
    {
        trackedRot = new CircularBuffer<float>();  //Circular buffer must be initialized in start method, it cannot use field initialization
        //buttonScript = gameObject.GetComponent<Button>();
        //buttonPressed = false;
        seesawScript = gameObject.GetComponent<PlankSeeSaw>();
    }


    //In this method define what will be tracked. In our case we want to track already implemented audio tracking,particle tracking + new custom timer tracking
    protected override void Track()
    {
        TrackRot();
        seesawScript.SetRewinding(false);
    }

    //In this method define, what will be restored on time rewinding. In our case we want to restore Particles, Audio and custom implemented Timer
    protected override void Rewind(float seconds)
    {
        RestoreRot(seconds);
        seesawScript.SetRewinding(true);
    }


    // This is an example of custom variable tracking
    public void TrackRot()
    {
        trackedRot.WriteLastValue(currRot);
    }


    // This is an example of custom variable restoring
    public void RestoreRot(float seconds)
    {
        float rewindValue = trackedRot.ReadFromBuffer(seconds);
        if (rewindValue != currRot) {
            seesawScript.SetCurrRot(rewindValue);
        }
        currRot = rewindValue;
        transform.rotation = Quaternion.Euler(currRot, 90f, 0f);
    }

    public void SerCurrRot(float rot)
    {
        currRot = rot;
    }
}
