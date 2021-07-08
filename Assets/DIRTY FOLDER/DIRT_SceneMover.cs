using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DIRT_SceneMover : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SceneManager.LoadScene(0);
        //}
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (SceneManager.GetActiveScene().buildIndex < 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        
    }
}
