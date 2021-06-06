using PlayerNS;
using UnityEngine;

namespace Hazards
{
    public class Hazard : MonoBehaviour
    {
        public string hazardName;
        public int damageValue;

        protected virtual void Damage(int damage)
        {
            Health.health -= damage;
        }
    }
}