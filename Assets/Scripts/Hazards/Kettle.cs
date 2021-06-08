using System;
using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Kettle : Hazard
    {
        private bool _isPlayerDetected;
        [SerializeField] private float _timer;
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
                _timer = 0f;
            }
        }

        private void Update()
        {
            if (_isPlayerDetected)
            {
                _timer += Time.deltaTime;
                if (_timer >= _timeOfExplosion)
                {
                    Damage(damageValue);
                    Destroy(gameObject);
                }
            }
        }
    }
}

