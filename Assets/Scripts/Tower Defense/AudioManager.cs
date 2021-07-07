using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //Setting up a singleton that doesn't get destroyed across scene loads.
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("GameManager Instance Presence, Destroying Copy.");
            Destroy(this.gameObject);
        }
    }

    //A bit memory hungry, but only makes sure all audio sources are paired up with a global audio script live. 
    //This makes sure that even if an object is instanciated past the first scene load they will get an global audio component.
    void Update()
    {
        AudioSource[] audioArray = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < audioArray.Length; i++)
        {
            if (audioArray[i].GetComponent<GlobalAudio>() == null)
            {
                audioArray[i].gameObject.AddComponent<GlobalAudio>();
            }
        }
    }
}
