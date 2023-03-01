using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptedEventAbstract
{
    public float time;
}

public abstract class ScriptedAbstract<T> : MonoBehaviour where T : ScriptedEventAbstract
{
    [SerializeField] private List<T> events;
    protected virtual bool IsInFixedUpdate => false; //set to true for physics events

    private int index;
    
    //Implements the actual event - screen shake or force application etc.
    protected abstract void TriggerEvent(T eventToTrigger);

    // Awake called before the first frame update
    protected virtual void Awake()
    {
        events.Sort((a, b) => a.time.CompareTo(b.time));
    }

    void Update()
    {
        if (!IsInFixedUpdate)
        {
            CheckForEventTrigger();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsInFixedUpdate)
        {
            CheckForEventTrigger();
        }
    }

    protected void CheckForEventTrigger()
    {
        //can be made more efficient with iterator probably
        //TODO: make it so you can apply multiple forces in one update
        if (index < events.Count)
        {
            T nextEvent = events[index];
            if (GameManager.Instance.Time >= nextEvent.time)
            {
                TriggerEvent(nextEvent);
                index++;
            }
        }
        if (index > 0)
        {
            T previousEvent = events[index - 1];
            if (GameManager.Instance.Time < previousEvent.time)
            {
                TriggerEvent(previousEvent);
                index--;
            }
        }
    }
}
