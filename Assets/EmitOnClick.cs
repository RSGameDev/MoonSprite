using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOnClick : MonoBehaviour
{
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ps.Emit(Random.Range(5, 10));
        }
    }
}
