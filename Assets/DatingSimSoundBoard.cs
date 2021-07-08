using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatingSimSoundBoard : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(int sound)
    {
        audio.clip = sounds[sound];
        audio.Play();
    }
}
