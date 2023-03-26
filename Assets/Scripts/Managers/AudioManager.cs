using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private MusicReverser musicReverser;
    [SerializeField] private SoundEffectTracker _worldSfxTracker;
    public SoundEffectTracker WorldSfxTracker => _worldSfxTracker;
    [SerializeField] private SoundEffectTracker _playerSfxTracker;
    public SoundEffectTracker PlayerSfxTracker => _playerSfxTracker;
    
    
    [SerializeField] private List<AudioClip> jumpSFX;
    
    public override void Awake()
    {
        base.Awake();
        musicReverser = GetComponent<MusicReverser>();
    }

    public void JumpSFX(AudioSource source)
    {
        int rand = Random.Range(0, jumpSFX.Count + 1);
        if (rand < jumpSFX.Count)
        {
            source.Stop();
            source.clip = jumpSFX[rand];
            source.pitch = 1 + Random.Range(-0.2f, 0.2f);
            source.volume = 0.4f;
            source.Play();
            
        }
    }
}
