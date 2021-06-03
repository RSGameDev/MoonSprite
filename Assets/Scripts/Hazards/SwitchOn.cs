using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOn : MonoBehaviour
{
    public GameObject[] stove;
    private bool isStoveOn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isStoveOn)
            {
                stove[1].gameObject.SetActive(false);
                isStoveOn = false;
                return;
            }
            stove[1].gameObject.SetActive(true);
            isStoveOn = true;
        }
    }
}
