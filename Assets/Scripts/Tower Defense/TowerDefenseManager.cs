using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    public int health = 4;
    public void DamagePlayer(int amount)
    {
        health += amount;
        Debug.Log("Player Health : " + health);
        if(health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }
}
