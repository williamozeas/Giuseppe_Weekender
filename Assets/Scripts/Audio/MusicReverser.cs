using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReverser : MonoBehaviour
{
    private AudioSource source;
    private SceneNum currentScene;

    [SerializeField] private AudioClip MenuMusic;
    [SerializeField] private AudioClip KitchenMusic;
    [SerializeField] private AudioClip EngineMusic;
    [SerializeField] private AudioClip CaptainMusic;
    
    [Header("Reversed")]
    [SerializeField] private AudioClip ReversedMenuMusic;
    [SerializeField] private AudioClip ReversedKitchenMusic;
    [SerializeField] private AudioClip ReversedEngineMusic;
    [SerializeField] private AudioClip ReversedCaptainMusic;

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
#if UNITY_WEBGL
            source.pitch = GameManager.Instance.Player.RewindRampWorld;
#else
            source.pitch = GameManager.Instance.Player.RewindRampWorld * -1;
#endif
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
        currentScene = newScene;
    }

    private void OnStopTime()
    {
        source.pitch = 0;
    }

    private void OnStartRewind()
    {
#if UNITY_WEBGL
        source.Stop();
        source.clip = GetSceneMusicReversed(currentScene);
        float time = 30f - GameManager.Instance.Time;
        source.timeSamples = Math.Max(0, (int)(time * 44100));
        Debug.Log("Setting to " + time);
        source.Play();
#else
        source.pitch = -1;
        Debug.Log("Incorrect");
#endif
    }

    private void OnStopRewind()
    {
#if UNITY_WEBGL
        source.clip = GetSceneMusic(currentScene);
#endif
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
        source.clip = clip;
        source.timeSamples = 0;
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
    
    public AudioClip GetSceneMusicReversed(SceneNum scene)
    {
        switch (scene)
        {
            case (SceneNum.MainMenu):
            {
                return ReversedMenuMusic;
            }
            default:
            case (SceneNum.Kitchen):
            {
                return ReversedKitchenMusic;
            }
            case (SceneNum.Engine):
            {
                return ReversedEngineMusic;
            }
            case (SceneNum.Captain):
            {
                return ReversedCaptainMusic;
            }
        }
    }
}
