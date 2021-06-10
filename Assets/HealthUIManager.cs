using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNS;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    private Slider slider;
    


    void Start()
    {
        slider = GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.health>=0&&Health.health<4)
        {
            slider.value = Health.health;
        }
        
    }
}
