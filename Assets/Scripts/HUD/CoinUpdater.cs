using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUpdater : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = gm.coins.ToString();
    }
}
