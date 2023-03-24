using UnityEngine;


//This script is showing setup of defaulty implemented tracking solutions (eg. tracking particles, audio...) in combination with custom variable tracking.
public class ButtonRewind : RewindAbstract
{
    CircularBuffer<bool> trackedBool;     //For storing data, use this CircularBuffer class
    Button buttonScript;

    bool buttonPressed;

    private void Start()
    {
        trackedBool = new CircularBuffer<bool>();  //Circular buffer must be initialized in start method, it cannot use field initialization
        buttonScript = gameObject.GetComponent<Button>();
        buttonPressed = false;
    }


    //In this method define what will be tracked. In our case we want to track already implemented audio tracking,particle tracking + new custom timer tracking
    protected override void Track()
    {
        TrackBool();
    }

    //In this method define, what will be restored on time rewinding. In our case we want to restore Particles, Audio and custom implemented Timer
    protected override void Rewind(float seconds)
    {
        RestoreBool(seconds);
    }


    // This is an example of custom variable tracking
    public void TrackBool()
    {
        trackedBool.WriteLastValue(buttonPressed);
    }


    // This is an example of custom variable restoring
    public void RestoreBool(float seconds)
    {
        bool rewindValue = trackedBool.ReadFromBuffer(seconds);
        if (rewindValue != buttonPressed) {
            buttonScript.StateChange(rewindValue);
        }
        buttonPressed = rewindValue;
    }

    public void StateChange(bool newState)
    {
        buttonPressed = newState;
    }
}
