using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : RewindAbstract
{
    private struct ShakeState
    {
        public float shakeTimer;
        public float startingIntensity;
        public float totalTime;
    }
    
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer = 0;
    private float startingIntensity = 0;
    private float totalTime = 0;
    
    private CircularBuffer<ShakeState> trackedState;

    protected override void Awake()
    {
        base.Awake();
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        trackedState = new CircularBuffer<ShakeState>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = time;
        totalTime = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                EasingFunction.EaseOutQuad(startingIntensity, 0f, 1 - (shakeTimer / totalTime));
        }
        else
        {
            startingIntensity = 0;
            shakeTimer = 0;
            totalTime = 1;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
    
    protected override void Track()
    {
        ShakeState state;
        state.shakeTimer = shakeTimer;
        state.startingIntensity = startingIntensity;
        state.totalTime = totalTime;
        trackedState.WriteLastValue(state);
    }

    //In this method define, what will be restored on time rewinding. In our case we want to restore Particles, Audio and custom implemented Timer
    protected override void Rewind(float seconds)
    {
        ShakeState newState = trackedState.ReadFromBuffer(seconds);
        shakeTimer = newState.shakeTimer;
        startingIntensity = newState.startingIntensity;
        totalTime = newState.totalTime;
    }
}
