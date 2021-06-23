using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TD_CoinCounter : MonoBehaviour
{
    TextMeshProUGUI text;
    public int coins;
    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        coins = 100;
        if (FindObjectOfType<GameManager>()!=null)
        {
            coins += FindObjectOfType<GameManager>().coins * 10;
        }
    }

    
    void Update()
    {
        text.text = coins.ToString();
    }
}
