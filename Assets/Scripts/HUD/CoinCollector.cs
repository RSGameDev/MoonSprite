using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private GameManager gm;
    public GameObject sound;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.coins++;
            Instantiate(sound, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
