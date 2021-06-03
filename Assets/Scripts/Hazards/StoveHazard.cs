using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveHazard : MonoBehaviour
{
    private Collider2D stoveCollider;
    public float DamageOverTime;
    private bool DamageOn;
    private GameObject player;

    private int temp;

    private bool isDOT;
    private bool isInstantDamage;

    private string stove = "StoveTopOn";
    private string knife = "Knife";
    private int knifeDmg = 20;

    // Start is called before the first frame update
    void Awake()
    {
        stoveCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    //private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            DamageOn = true;
            if (gameObject.name == knife)
            {
                InstantDamage(knifeDmg);
            }
        }
    }

    private void Update()
    {
        if (DamageOn)
        {
            if (gameObject.name == stove)
            {
                player.GetComponent<Health>().health -= Time.deltaTime * DamageOverTime;
            }
        }
    }

    void InstantDamage(int damage)
    {
        player.GetComponent<Health>().health -= damage;
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

