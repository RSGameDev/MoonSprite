using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBCam : MonoBehaviour
{
    //This is a script responsible for controling the "Index based camera controls" 
    //Primarily used in the platforming level.

    public Transform[] locations;
    public int index;
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(locations[index].position, transform.position) >= 0.3f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, locations[index].position, step);
        }
    }
}
