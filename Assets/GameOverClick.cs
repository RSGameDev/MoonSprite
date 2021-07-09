using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib.Stage;
using UnityEngine.SceneManagement;

public class GameOverClick : MonoBehaviour
{
    //public void Start()
    //{
    //    gameObject.SetActive(false);
    //}
    public void ResetPressed()
    {
        Time.timeScale = 1f;
        SceneControls.Restart();
    }
    public void MainMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    
    //void Update()
    //{
    //    if (Input.anyKeyDown)
    //    {

    //        Time.timeScale = 1f;
    //        GameManager.instance.ResetScene();

    //    }
    //}
}
