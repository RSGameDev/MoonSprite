using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib.Stage;

public class GameManagerSoundLibrary : MonoBehaviour
{
    public AudioClip[] music;

    public static GameManagerSoundLibrary instance;

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

    private void Update()
    {
        if (GetComponent<AudioSource>().clip != music[SceneControls.Read()])
        {
            GetComponent<AudioSource>().clip = music[SceneControls.Read()];
            GetComponent<AudioSource>().Play();
        }
    }
}
