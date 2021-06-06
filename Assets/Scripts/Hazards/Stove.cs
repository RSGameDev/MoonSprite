using System;
using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Stove : Hazard
    {
        private bool _isPlayerDetected;
        private float _timer;

        protected override void Damage(int damage)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0;
                Health.health -= damage;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Health>())
            {
                _isPlayerDetected = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Health>())
            {
                _isPlayerDetected = false;
            }
        }
        
        private void Update()
        {
            if (_isPlayerDetected)
            {
                Damage(damageValue);
            }
        }
    }
}

