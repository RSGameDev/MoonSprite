using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    Transform mouse;
    public float strength;
    public float range;
    public float pickRange;
    private Rigidbody2D rb;

    private bool _isPlayingSfx;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mouse = GameObject.FindGameObjectWithTag("Mouse").transform;
        rb.velocity = new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, mouse.position)<range)
        {
            rb.AddForce((mouse.position - transform.position) * strength);
            if (Vector2.Distance(transform.position, mouse.position) < pickRange)
            {
                if (!_isPlayingSfx)
                {
                    print("playing");
                    _isPlayingSfx = true;
                    GetComponentInChildren<AudioSource>().Play();
                }
                print(_isPlayingSfx);
                FindObjectOfType<TD_CoinCounter>().coins += 10;
                GetComponent<SpriteRenderer>().enabled = false;
                //StartCoroutine(DestroyCoin());
                
            }
        }
    }

    IEnumerator DestroyCoin()
    {
        yield return new WaitForSeconds(1.670f);
        Destroy(this.gameObject);
    }
    

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
