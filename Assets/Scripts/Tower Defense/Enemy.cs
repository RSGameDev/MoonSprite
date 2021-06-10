using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private float speed;
    private float health;

    public List<Vector2> route;
    public int routeIndex = 0;


    public bool nextRoute = true;

    private float nextIndexDistance = 0;
    public float lerpValue = 0;
    public float frameIncrement;

    private float deadZone = 0.00f; // How close to the checkpoint before moving on

    private bool setup = false;

    public void SetupEnemy(EnemyData data, List<Vector2> inRoute)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.sprite;

        route = inRoute;
        speed = data.speed;
        health = data.health;

        transform.position = route[0];
        setup = true;
    }

    private void Update()
    {
        
        if (!setup) return;
        if (routeIndex >= route.Count)
        {
            return;
        }

        if (nextRoute)
        {
            routeIndex++;
            if (routeIndex >= route.Count)
            {
               // nextRoute = false;
                // REACHED END
                //Destroy(this);
            }
            else
            {               
                nextIndexDistance = Vector2.Distance(route[routeIndex - 1], route[routeIndex]);
                nextRoute = false;
                lerpValue = 0;
            }
           
        }
        else
        {
            //Debug.LogError("HERE");
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if(lerpValue >= 1 - deadZone)
        {
            nextRoute = true;
        }
        else
        {
            CalculateLerp();
            
            Vector2 position = Vector2.Lerp(route[routeIndex - 1], route[routeIndex], lerpValue);
            transform.position = position;
        }
    }

    private void CalculateLerp()
    {
        frameIncrement = (Time.deltaTime / nextIndexDistance) * speed;
        lerpValue += frameIncrement;
        lerpValue = Mathf.Clamp(lerpValue, 0, 1);
    }
}
