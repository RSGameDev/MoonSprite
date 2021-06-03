using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject playerHealth;
    private TextMeshProUGUI playerHealthText;

    public Health playerHealthScript;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerHealthText = playerHealth.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        playerHealthText.SetText("100");
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.SetText(playerHealthScript.health.ToString("0"));
    }
}
