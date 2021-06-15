using Unity.Mathematics;
using UnityEngine;

namespace Tower_Defense.Towers
{
    public class TowerDeploy : MonoBehaviour
    {
        public Towers tower;
    
        public Sprite towerImage;
        public Sprite projectileImage;
        public int health;
        public int damage;

        public GameObject bullets;
        private GameObject enemy;

    
    
        private void Start()
        {
            towerImage = tower.towerImage;
            GetComponent<SpriteRenderer>().sprite = towerImage;
            projectileImage = tower.projectileImage;
            health = tower.health;
            damage = tower.damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            enemy = other.gameObject;
            var newBullet = Instantiate(bullets, transform.position, quaternion.identity);
            newBullet.GetComponent<Projectile>().AssignTarget(enemy);
            newBullet.GetComponent<Projectile>().AssignProjectile(projectileImage);
        }

    }
}
