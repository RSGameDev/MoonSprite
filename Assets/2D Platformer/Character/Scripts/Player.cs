using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float speed = 8f; 
    [SerializeField] private float coyoteDuration = 0.05f;  
    [SerializeField] private float maxFallSpeed = -25f;

    [Header("Jump Properties")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float jumpHoldForce = 2f;
    [SerializeField] private float jumpHoldDuration = 0.1f;

    [Header ("Enviroment Check Properties")]
    [SerializeField] private float footOffset = 0.5f;
    [SerializeField] private float headClearance = 0.5f;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header ("Status Flags")]
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isJumping;

    //Crouch Functionality
    //[SerializeField] private bool isHeadBlocked;
    //private float playerHeight;

    private Rigidbody2D rigidBody;
    private BoxCollider2D bodyCollider;
   

    private int direction = 1;
    private float coyoteTime;
    private float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        
        // Crouch Funtionality
       // playerHeight = bodyCollider.size.y;
    }

    //Run Physics calculations
    private void FixedUpdate()
    {
        PhysicsCheck();

        GroundMovement();

        AirMovement();
    }

    private void PhysicsCheck()
    {
        isOnGround = false;        

        //Check if either foot is on the ground
        RaycastHit2D leftCheck = Physics2D.Raycast(new Vector2(transform.position.x -footOffset, transform.position.y), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Physics2D.Raycast(new Vector2(transform.position.x + footOffset, transform.position.y), Vector2.down, groundDistance, groundLayer);

        //Debug rays
        Debug.DrawRay(new Vector3(transform.position.x - footOffset, transform.position.y, transform.position.z), Vector3.down * groundDistance, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + footOffset, transform.position.y, transform.position.z), Vector3.down * groundDistance, Color.green);

        if ( leftCheck || rightCheck)
        {
            isOnGround = true;
        }

        //Functionality for a crouch if needed
        //isHeadBlocked = false;
        //RaycastHit2D headCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + playerHeight), Vector2.up, headClearance, groundLayer);
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + playerHeight, transform.position.z), Vector3.up * headClearance, Color.blue);

        //if (headCheck)
        //{
        //    isHeadBlocked = true;
        //}
    }
    private void GroundMovement()
    {
        float xVelocity = speed * Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        if(xVelocity * direction < 0f)
        {
            FlipDirection();
        }
        if (isOnGround)
        {
            coyoteTime = Time.time + coyoteDuration;
        }

    }
    private void AirMovement()
    {
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if(Input.GetButton("Jump") && !isJumping && (isOnGround || coyoteTime > Time.time))
        {
            isOnGround = false;
            isJumping = true;

            jumpTime = Time.time + jumpHoldDuration;

            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
           
        }
        else if (isJumping)
        {
            if (Input.GetButton("Jump"))
            {
                rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            }

            if(jumpTime <= Time.time || Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        }

        if(rigidBody.velocity.y < maxFallSpeed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
        }
    }

    private void FlipDirection()
    {
       
        direction *= -1;
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }
}
