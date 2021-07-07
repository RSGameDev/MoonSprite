using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalAudio : MonoBehaviour
{
    //These variables exist across all instances of global audio, changing them updates the aduiosource on the gameobject accordingly.
    public static float volume = 1;
    public static bool mute = false;

   
    private AudioSource audioComponent;

    //Instead of changing the volume directly, we're taking a starting volume and modifying it according to the global volume.
    //This allows control over audio, while not sacrificing volume depth within the scene
    //Aka, music can be quiet and jump can be at full volume, but if global volume is set to .4f they both scale accordingly.
    private float startingVolume;
    void Start()
    {
        audioComponent = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        //By calling for a change only if the values don't match up we're making sure not to waste any memory. 
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
