using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        if (ps.trigger.GetCollider(0))
        {
            Debug.Log(ps.trigger.GetCollider(1).gameObject.name);
        }   
    }
}
