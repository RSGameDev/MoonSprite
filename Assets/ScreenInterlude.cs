using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenInterlude : MonoBehaviour
{
    private float _timer;
    private Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (currentScene.name == "SceneInterlude1" && _timer >= 18)
        {
            var scene = SceneController._instance.firstLevel;
            SceneController._instance.LoadLevel(scene);
        }

        if (currentScene.name == "SceneInterlude2")
        {
            if (_timer >= 7)
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        
        if (currentScene.name == "SceneInterlude2" && _timer >= 17)
        {
            var scene = SceneController._instance.secondLevel;
            SceneController._instance.LoadLevel(scene);
        }

        if (currentScene.name == "SceneInterlude3" && _timer >= 17)
        {
            var scene = SceneController._instance.thirdLevel;
            SceneController._instance.LoadLevel(scene);
        }
    }
}