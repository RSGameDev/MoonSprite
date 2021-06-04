using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Properties")]
    public float speed = 8f;
    public float coyoteDuration = 0.05f;
    public float maxFallSpeed = -25f;

    [Header("Jump Properties")]
    public float jumpForce = 6f;
    public float jumpHoldForce = 2f;
    public float jumpHoldDuration = 0.1f;

    [Header ("Enviroment Check Properties")]
    public float footOffset = 0.5f;
    public float headClearance = 0.5f;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    [Header ("Status Flags")]
    public bool isOnGround;
    public bool isJumping;
    public bool isHeadBlocked;

    private Rigidbody2D rigidBody;
    private BoxCollider2D bodyCollider;
    private float playerHeight;
    private Vector2 colliderSize;

    private int direction = 1;
    private float coyoteTime;
    private float jumpTime;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        colliderSize = bodyCollider.size;
        playerHeight = bodyCollider.size.y;
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
        isHeadBlocked = false;

        //Check if either foot is on the ground
        RaycastHit2D leftCheck = Physics2D.Raycast(new Vector2(transform.position.x -footOffset, transform.position.y), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Physics2D.Raycast(new Vector2(transform.position.x + footOffset, transform.position.y), Vector2.down, groundDistance, groundLayer);

        Debug.DrawRay(new Vector3(transform.position.x - footOffset, transform.position.y, transform.position.z), Vector3.down * groundDistance, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + footOffset, transform.position.y, transform.position.z), Vector3.down * groundDistance, Color.green);

        if ( leftCheck || rightCheck)
        {
            isOnGround = true;
        }

        RaycastHit2D headCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + playerHeight), Vector2.up, headClearance, groundLayer);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + playerHeight, transform.position.z), Vector3.up * headClearance, Color.blue);
        
        if (headCheck)
        {
            isHeadBlocked = true;
        }
    }
    private void GroundMovement()
    {
        float xVelocity = speed * Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
       // Debug.LogError("Direction : " + direction + " Veloctiy : " + xVelocity * direction);
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
        //Debug.Log("FLIPPED");
        direction *= -1;
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }
}
