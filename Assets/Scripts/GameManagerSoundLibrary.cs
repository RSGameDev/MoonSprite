using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib.Stage;

public class GameManagerSoundLibrary : MonoBehaviour
{
    public AudioClip[] music;

    private void Update()
    {
        if (GetComponent<AudioSource>().clip != music[SceneControls.Read()])
        {
            GetComponent<AudioSource>().clip = music[SceneControls.Read()];
            GetComponent<AudioSource>().Play();
        }
    }
}
