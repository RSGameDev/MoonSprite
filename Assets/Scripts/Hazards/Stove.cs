using System;
using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Stove : Hazard
    {
        private bool _isPlayerDetected;
        private float _timer;
        private Transform player;

        public float damageDis;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        protected override void Damage(int damage)
        {
            
            if (_timer >= .8f)
            {
                _timer = 0;
                Health.health -= damage;
            }
        }


        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.GetComponent<Health>())
        //    {
        //        _isPlayerDetected = true;
        //    }
        //}
        
        //private void OnTriggerExit2D(Collider2D other)
        //{
        //    if (other.gameObject.GetComponent<Health>())
        //    {
        //        _isPlayerDetected = false;
        //    }
        //}
        
        private void Update()
        {
            _timer += Time.deltaTime; //having it inside the damage function lead to the timer going down only if the player would be damaged.
            if (Vector3.Distance(transform.position, player.position) <= damageDis)
            {
                Damage(damageValue);
            }
            //if (_isPlayerDetected)
            //{
            //    Damage(damageValue);
            //}
        }
    }
}

