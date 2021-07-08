using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatingSimMusicManager : MonoBehaviour
{
    
    public NarrativeController nc;
    
    public GlobalAudio[] _audio;
    public int musicIndex;
    public float rate;

    void Start()
    {
        musicIndex = 1;
        
        
    }

    private void Update()
    {
        for (int i = 0; i < _audio.Length; i++)
        {
            if (_audio[i] != _audio[musicIndex])
            {
                if (_audio[i].startingVolume >= 0)
                {
                    _audio[i].startingVolume -= rate * Time.deltaTime;
                }
            }
            if ((_audio[i] == _audio[musicIndex]) && (_audio[i].startingVolume<=0.12f))
            {

                _audio[i].startingVolume += rate * Time.deltaTime;
            }
            
        }
    }


    public void Change(int track) 
    {
        musicIndex = track;
    }
}
