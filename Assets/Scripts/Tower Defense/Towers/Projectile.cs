using UnityEngine;
using PipLib;

namespace Tower_Defense.Towers
{
    public class Projectile : MonoBehaviour
    {
        private float speed = 10.0f;
        private Vector2 enemyPos;
        private Rigidbody2D rb;
        public int damage;

        public Sprite projectileImage;
    
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<SpriteRenderer>().sprite = projectileImage;
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float step = speed * Time.deltaTime;
            rb.velocity = transform.up * speed;
            
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position)<0.4f)
                {
                    enemies[i].Hurt(damage);
                    Destroy(gameObject);
                }
            }
        }

        public void AssignTarget(GameObject enemy)
        {
            transform.eulerAngles = new Vector3(0, 0, Util.PointToward(enemy, this.gameObject));
            
        }
    
        public void AssignProjectile(Sprite projectile)
        {
            projectileImage = projectile; 
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }

   
}
