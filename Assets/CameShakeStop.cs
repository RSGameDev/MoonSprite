using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameShakeStop : MonoBehaviour
{
    Animator anim;
    bool shake;
    float t;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        shake = anim.GetBool("Shake");
        if (shake)
        {
            if (t<0.5f)
            {
                t += Time.deltaTime;
            }
            else
            {
                t = 0;
                anim.SetBool("Shake", false);
            }
        }
    }
}
