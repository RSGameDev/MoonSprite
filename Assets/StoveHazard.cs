using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveHazard : MonoBehaviour
{
    private Collider2D stove;
    public float DamageOverTime;
    private bool DamageOn;
    private GameObject player;

    private int temp;

    // Start is called before the first frame update
    void Awake()
    {
        stove = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    //private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            DamageOn = true;
        }
    }

    private void Update()
    {
        if (DamageOn)
        {
            player.GetComponent<Health>().health -= Time.deltaTime * DamageOverTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            DamageOn = false;
        }
    }
}

