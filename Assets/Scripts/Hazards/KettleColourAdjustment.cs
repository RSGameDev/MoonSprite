using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hazards;

public class KettleColourAdjustment : MonoBehaviour
{
    public Color spriteColour;
    private SpriteRenderer sr;
    private Kettle kettle;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        kettle = GetComponent<Kettle>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = spriteColour;
        
        if (kettle._isPlayerDetected)
        {
            
            spriteColour.g -= .8f*Time.deltaTime;
            spriteColour.b -= .8f * Time.deltaTime;
            
        }
        
        if (!kettle._isPlayerDetected)
        {
            
            spriteColour.g = 1;
            spriteColour.b = 1;
        }


    }
}
