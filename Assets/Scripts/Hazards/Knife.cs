using System;
using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Knife : Hazard
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Health>())
            {
                Damage(damageValue);
            }
        }
    }
}

