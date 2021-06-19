using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib.Stage;

public class GameManager : MonoBehaviour
{

    //This makes sure that this is a singleton.
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("GameManager Instance Presence, Destroying Copy.");
            Destroy(this.gameObject);
        }
    }

    public int coins;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        coins = 0;
        SceneControls.Restart();
    }
}
