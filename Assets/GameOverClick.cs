using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib.Stage;

public class GameOverClick : MonoBehaviour
{
   
    

    
    void Update()
    {
        if (Input.anyKeyDown)
        {

            Time.timeScale = 1f;
            GameManager.instance.ResetScene();

        }
    }
}
