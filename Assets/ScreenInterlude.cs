using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenInterlude : MonoBehaviour
{
    private float _timer;
    
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 15)
        {
            var scene = SceneController._instance.secondLevel;
            SceneController._instance.LoadLevel(scene);
        }
    }
}
