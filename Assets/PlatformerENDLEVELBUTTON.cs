using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PipLib.Stage;

public class PlatformerENDLEVELBUTTON : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneControls.NextLevel();
        }
    }
}
