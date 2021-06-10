using System;
using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Kettle : Hazard
    {
        public bool _isPlayerDetected;
        public float timer;
        [SerializeField] private float _timeOfExplosion;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerDetected = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isPlayerDetected = false;
                timer = 0f;
            }
        }

        private void Update()
        {
            if (_isPlayerDetected)
            {
                timer += Time.deltaTime;
                if (timer >= _timeOfExplosion)
                {
                    Damage(damageValue);
                    Destroy(gameObject);
                }
            }
        }
    }
}

