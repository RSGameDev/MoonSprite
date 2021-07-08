using System;
using System.Collections;
using System.Collections.Generic;
using PlayerNS;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    [SerializeField] private HUD.HUD _hud;
    public int health;
    public GameObject GameOverScreen;

    private void Awake()
    {
        Health.health = health;
    }

    private void Start()
    {
        Health.health = 10;
    }

    public void DamagePlayer(int amount)
    {
        Health.health += amount;
        Debug.Log("Player Health : " + health);
        if(Health.health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("GAME OVER");
    }
}
