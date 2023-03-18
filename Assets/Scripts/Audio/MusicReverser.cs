using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReverser : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        source.Play();
    }

    void FixedUpdate()
    {
        if (RewindManager.IsBeingRewinded && Input.GetButton("Rewind World"))
        {
            source.pitch = GameManager.Instance.Player.RewindRampWorld * -1;
        }
    }

    private void OnEnable()
    {
        RewindManager.StopTime += OnStopTime;
        RewindManager.StartRewind += OnStartRewind;
        RewindManager.StopRewind += OnStopRewind;
    }

    private void OnDisable()
    {
        RewindManager.StopTime -= OnStopTime;
        RewindManager.StartRewind -= OnStartRewind;
        RewindManager.StopRewind -= OnStopRewind;
    }

    private void OnStopTime()
    {
        source.pitch = 0;
    }

    private void OnStartRewind()
    {
        source.pitch = -1;
    }

    private void OnStopRewind()
    {
        source.timeSamples = Math.Max(0, (int)(GameManager.Instance.Time * 44100));
        source.pitch = 1;
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
