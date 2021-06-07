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
        if (other.GetComponent<Player>())
        {
            Health.health--;
            other.gameObject.transform.position = _gameObjects.transform.GetChild(0).transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
