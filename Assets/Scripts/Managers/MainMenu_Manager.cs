using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject CreditsMenu;


    public void PlayPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void CreditsPressed()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void OptionsPressed()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void ExitPressed()
    {
        Application.Quit();
    }

    public void OptionsBack()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void CreditsBack()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ChangedSlider(float val)
    {
        GlobalAudio.volume = val;
        //Debug.Log(val);
    }

    
}
