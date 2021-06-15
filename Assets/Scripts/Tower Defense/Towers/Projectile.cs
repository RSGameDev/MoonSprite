using UnityEngine;

namespace Tower_Defense.Towers
{
    public class Projectile : MonoBehaviour
    {
        private float speed = 10.0f;
        private Vector2 enemyPos;

        public Sprite projectileImage;
    
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<SpriteRenderer>().sprite = projectileImage;
        }

        // Update is called once per frame
        void Update()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, enemyPos, step);
            if ((Vector2)transform.position == enemyPos)
            {
                Destroy(gameObject);
            }
        }

        public void AssignTarget(GameObject enemy)
        {
            enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
        }
    
        public void AssignProjectile(Sprite projectile)
        {
            projectileImage = projectile; 
        }
    }
}
