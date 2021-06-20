using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOneEnd : MonoBehaviour
{
    AudioSource a;
    void Start()
    {
        a = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (!a.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
