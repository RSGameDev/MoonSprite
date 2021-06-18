using System;
using System.Collections;
using System.Collections.Generic;
using PlayerNS;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public static GameObject _gameObjects;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Health>().Hurt();
            other.gameObject.transform.position = _gameObjects.transform.GetChild(0).transform.position;
        }
    }

    
}
