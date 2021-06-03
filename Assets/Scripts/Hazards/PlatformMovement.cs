using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject[] platforms;
    private Vector3[] positions;

    public float speed = 1.0f;

    private bool isTowards = true;
    
    private void Awake()
    {
        positions = new Vector3[platforms.Length];
        positions[0] = platforms[0].transform.position;
        positions[1] = platforms[1].transform.position;
    }

    private void Update()
    {
        if (isTowards)
        {
            // Move our position a step closer to the target.
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, positions[1], step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, positions[1]) < 0.001f)
            {
                isTowards = false;
            } 
        }
        else
        {
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, positions[0], step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, positions[0]) < 0.001f)
            {
                isTowards = true;
            }
        }
    }
}
