using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBCamMod : MonoBehaviour
{
    public IBCam cam;
    public float reload;
    private float t;
    public int mod;

    void Update()
    {
        if (t>=0)
        {
            t -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            if (t <= 0)
            {
                t = reload;
                cam.index += mod;
            }
        }
        
        
    }

}
