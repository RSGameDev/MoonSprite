using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private Rigidbody2D rb;

   // private GameObject 


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalMovement != 0)
        {
            rb.AddForce(new Vector2(horizontalMovement * Time.deltaTime * MovementSpeed, 0));
        }

        float verticalMovement = Input.GetAxis("Jump");
        if (verticalMovement != 0)
        {
            
            rb.AddForce(new Vector2(0, Time.deltaTime * JumpHeight));
        }
    }
}
