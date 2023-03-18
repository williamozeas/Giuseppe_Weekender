using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private MusicReverser musicReverser;
    
    public override void Awake()
    {
        base.Awake();
        musicReverser = GetComponent<MusicReverser>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
