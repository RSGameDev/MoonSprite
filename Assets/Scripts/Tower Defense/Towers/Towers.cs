using UnityEngine;

namespace Tower_Defense.Towers
{
    [CreateAssetMenu(fileName = "Tower", menuName = "Towers",order = 1)]
    public class Towers : ScriptableObject
    {
        public string towerName = "New Tower";
        public Sprite towerImage;
        public Sprite projectileImage;
        public int health;
        public int damage;
    }
}
