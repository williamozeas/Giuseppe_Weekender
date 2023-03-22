using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReverser : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private AudioClip MenuMusic;
    [SerializeField] private AudioClip KitchenMusic;
    [SerializeField] private AudioClip EngineMusic;
    [SerializeField] private AudioClip CaptainMusic;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetMusic(GetSceneMusic(GameManager.Instance.CurrentScene));
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
        GameManager.OnLoadScene += OnLoadScene;
    }

    private void OnDisable()
    {
        RewindManager.StopTime -= OnStopTime;
        RewindManager.StartRewind -= OnStartRewind;
        RewindManager.StopRewind -= OnStopRewind;
        GameManager.OnLoadScene -= OnLoadScene;
    }

    private void OnLoadScene(SceneNum newScene)
    {
        SetMusic(GetSceneMusic(newScene));
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

    public void SetMusic(AudioClip clip)
    {
        source.Stop();
        Debug.Log("Here");
        source.clip = clip;
        source.Play();
    }

    public AudioClip GetSceneMusic(SceneNum scene)
    {
        switch (scene)
        {
            case (SceneNum.MainMenu):
            {
                return MenuMusic;
            }
            default:
            case (SceneNum.Kitchen):
            {
                return KitchenMusic;
            }
            case (SceneNum.Engine):
            {
                return EngineMusic;
            }
            case (SceneNum.Captain):
            {
                return CaptainMusic;
            }
        }
    }
}