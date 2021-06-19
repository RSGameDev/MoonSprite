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
        public Enemy[] enemies;
        public GameObject closetEnemy;

        private float t;
    
    
        private void Start()
        {
            towerImage = tower.towerImage;
            GetComponent<SpriteRenderer>().sprite = towerImage;
            projectileImage = tower.projectileImage;
            health = tower.health;
            damage = tower.damage;
        }


        private void Update()
        {
            if (t<tower.fireRate)
            {
                t += Time.deltaTime;
            }
           
            enemies = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(transform.position, enemies[i].transform.position)<=tower.range)
                {
                    if (closetEnemy == null)
                    {
                        closetEnemy = enemies[i].gameObject;
                    }
                    else
                    {
                        if (Vector2.Distance(transform.position, enemies[i].transform.position) < Vector2.Distance(transform.position, closetEnemy.transform.position))
                        {
                            closetEnemy = enemies[i].gameObject;
                        }
                    }
                }
            }
            if (t >=tower.fireRate&&closetEnemy!=null)
            {
                t = 0;
                var newBullet = Instantiate(bullets, transform.position, quaternion.identity);
                newBullet.GetComponent<Projectile>().AssignTarget(closetEnemy);
                newBullet.GetComponent<Projectile>().AssignProjectile(projectileImage);
            }
        }
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    enemy = other.gameObject;
        //    var newBullet = Instantiate(bullets, transform.position, quaternion.identity);
        //    newBullet.GetComponent<Projectile>().AssignTarget(enemy);
        //    newBullet.GetComponent<Projectile>().AssignProjectile(projectileImage);
        //}

    }
}
