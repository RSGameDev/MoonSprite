using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float input;
    public float speedMax;
    public float speedMin;
    public float accelerationPercent;
    public float jump;

    //speed varaible that allows for smoother acceleration
    [HideInInspector]
    public float speed;
   
    //bool variable which is used for flipping the player to the correct direction.
    private bool facingLeft = false;

    [HideInInspector]
    public bool isGrounded;
    public Transform feet;
    public float checkRadius;
    public LayerMask groundLayer;
    private bool readyToLand;

    //AudioSource Management;
    public AudioSource[] audioSources;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    
    void FixedUpdate()
    {
        //Recieving the player's Horizontal Axis as input (A&D, and arrow keys).
        input = Input.GetAxisRaw("Horizontal");
        //Checking if the player is actually inputting anything in order to make sure the velocity is not pinned.
        if (Mathf.Abs(input) >= 0.1f && Mathf.Abs(rb.velocity.x) <= speedMax)
        {
            //Instead of setting the velocity, velocity is modified, in order to allow for better transition. 
            rb.velocity += new Vector2(input * speed, 0);

            //This section controls for acceleration. 
            if (speed <= speedMax)
            {
                speed += accelerationPercent/100f;
            }
        }
        else 
        {
            //This section controls the decceleration of the acceleration number. 
            if (speed >= speedMin)
            {
                speed -= accelerationPercent / 100f;
            }
            
        }

        //Flip controls, along with momentum controls.
        if (facingLeft && input >0)
        {
            Flip();
            speed = speedMin;
        }else if(facingLeft == false && input < 0)
        {
            Flip();
            speed = speedMin;
        }


        //Jumping
        isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, groundLayer);
        input = Input.GetAxis("Vertical");
        if (input>0.2f&&isGrounded)
        {
            audioSources[1].Play();
            GetComponent<CharacterAnimUpdater>().Jump();
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        
        if (!isGrounded)
        {
            readyToLand = true;
        }
        if (readyToLand && isGrounded)
        {
            audioSources[2].Play();
            readyToLand = false;
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 ScaleRef = transform.localScale;
        ScaleRef.x *= -1;
        transform.localScale = ScaleRef;
    }
}
