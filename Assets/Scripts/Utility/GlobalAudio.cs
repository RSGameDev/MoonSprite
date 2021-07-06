using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalAudio : MonoBehaviour
{
    public static float volume = 1;
    public static bool mute = false;
    private AudioSource audioComponent;
    private float startingVolume;
    void Start()
    {
        audioComponent = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        if (startingVolume*volume!=audioComponent.volume)
        {
            audioComponent.volume = startingVolume * volume;
        }
        if (audioComponent.mute !=mute)
        {
            audioComponent.mute = mute;
        }

    }
}
