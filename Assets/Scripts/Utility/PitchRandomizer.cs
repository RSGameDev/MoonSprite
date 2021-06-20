using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    float originalPitch;
    // Start is called before the first frame update
    void Start()
    {
        originalPitch = GetComponent<AudioSource>().pitch;
        GetComponent<AudioSource>().pitch = originalPitch + Random.Range(-.2f, .2f);
    }

    
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().pitch = originalPitch + Random.Range(-.2f, .2f);
        }
        
    }
}
