using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimUpdater : MonoBehaviour
{
    PlayerMovement pm;
    Animator anim;
    Rigidbody2D rb;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        anim.SetFloat("Speed", pm.speed);
        anim.SetFloat("Y-Velocity", rb.velocity.y);       
        anim.SetFloat("Acceleration", Mathf.Clamp(pm.speed,0,2));        
        anim.SetBool("Grounded", pm.isGrounded);

    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
}
