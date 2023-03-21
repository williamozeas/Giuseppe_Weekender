using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watchable : MonoBehaviour
{

    Rigidbody rb;

    WatchableRewind watchScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        watchScript = GetComponent<WatchableRewind>();
    }

    public void Watched()
    {
        watchScript.Watched();
    }

    public void Unwatched()
    {
        watchScript.Unwatched();
    }
}
