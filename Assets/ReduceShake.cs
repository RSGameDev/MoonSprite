using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceShake : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("ShakeTime")>=0.1)
        {
            float t = anim.GetFloat("ShakeTime");
            t -= Time.deltaTime;
            anim.SetFloat("ShakeTime", t);
        }
    }
}
