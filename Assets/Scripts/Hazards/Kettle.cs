using System;
using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Kettle : Hazard
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Health>())
            {
                Damage(damageValue);
                Destroy(gameObject);
            }
        }
    }
}

