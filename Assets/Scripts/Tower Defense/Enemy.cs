using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private float speed;
    private int health;
    private float size;

    public List<Vector2> route;
    public int routeIndex = 0;

    public bool nextRoute = true;

    private float nextIndexDistance = 0;
    public float lerpValue = 0;
    public float frameIncrement;

    private float deadZone = 0.00f; // How close to the checkpoint before moving on

    private bool setup = false;
    private TowerDefenseManager tdManager;

    public void SetupEnemy(EnemyData data, List<Vector2> inRoute, TowerDefenseManager inTdManager) // Set the enemy up so all data is stored in this
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.sprite;

        route = inRoute;
        speed = data.speed;
        health = data.health;
        size = data.size;

        transform.localScale = new Vector3(size, size, 1);
        transform.position = route[0];
        setup = true;

        tdManager = inTdManager;
    }

    private void Update()
    {


        //if (routeIndex >= route.Count) // Are we done? stop all computation
        //{
        //    DamagePlayer();
        //    Destroy(gameObject);
        //}

        if (nextRoute) //Have I reached the next path point
        {
            routeIndex++;
            if (routeIndex >= route.Count)
            {
                DamagePlayer();
                Destroy(gameObject);
            }
            else
            {               
                nextIndexDistance = Vector2.Distance(route[routeIndex - 1], route[routeIndex]); // Get the distance between this and the next node
                nextRoute = false;
                lerpValue = 0;
            }
           
        }
        else
        {

            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if(lerpValue >= 1 - deadZone) // Are we there yet?
        {
            nextRoute = true;
        }
        else
        {
            CalculateLerp(); // Work out the lerp value so the enemy always moves a the same speed
            
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

    private void DamagePlayer()
    {
        Debug.Log("PLAYER DAMAGED");
        tdManager.DamagePlayer(-1);
    }
    
    public void Hurt(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
