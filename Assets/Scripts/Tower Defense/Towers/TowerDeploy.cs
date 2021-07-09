using Unity.Mathematics;
using UnityEngine;
using PipLib;

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

        public AudioSource[] audioSources;
        
        private float t;
    
    
        private void Start()
        {
            towerImage = tower.towerImage;
            GetComponent<SpriteRenderer>().sprite = towerImage;
            projectileImage = tower.projectileImage;
            health = tower.health;
            damage = tower.damage;
            bullets = tower.projectile;
        }


        private void Update()
        {
            print(tower.towerName);
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
            if (tower.towerName != "Oven")
            {
                if (t >= tower.fireRate && closetEnemy != null && Vector2.Distance(transform.position, closetEnemy.transform.position) < tower.range)
                {
                    t = 0;
                    var newBullet = Instantiate(bullets, transform.position, quaternion.identity);
                    if (tower.towerName == "rapid fire")
                    {
                        audioSources[0].Play();
                    }

                    if (tower.towerName == "Kettle")
                    {
                        if (!audioSources[1].isPlaying)
                        {
                            audioSources[1].Play();
                        }
                    }
                    newBullet.GetComponent<Projectile>().AssignTarget(closetEnemy);
                    newBullet.GetComponent<Projectile>().AssignProjectile(projectileImage);
                    newBullet.GetComponent<Projectile>().damage = damage;
                }
            }
            else
            {
                if (t >= tower.fireRate && closetEnemy != null && Vector2.Distance(transform.position, closetEnemy.transform.position) < tower.range)
                {
                    t = 0;
                    GetComponentInChildren<ParticleSystem>().Emit(1);
                    Debug.Log("pew");
                    
                    
                }
            }
            
            if (Vector2.Distance(transform.position, closetEnemy.transform.position) > tower.range)
            {
                audioSources[1].Stop();
            }
        }
    }
}
