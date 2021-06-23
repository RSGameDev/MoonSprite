using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tower_Defense.Towers;
using TMPro;

public class TowerPriceDisplay : MonoBehaviour
{
    public Towers _tower;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = _tower.priceRunTime.ToString();
    }
}
