using Managers;
using UnityEngine;

namespace PlayerNS
{
    public class Health : MonoBehaviour
    {
        public static int health = 3;
        public Animator anim;
        public AudioSource sound;

        public void Update()
        {
            if (health <= 0)
            {
                health = 3;
                PlayerDeath();
                //FindObjectOfType<GameManager>().ResetScene();
            }
            if (anim.GetFloat("ShakeTime")>=0)
            {
                float t = anim.GetFloat("ShakeTime");
                t -= Time.deltaTime;
                anim.SetFloat("ShakeTime", t);
            }
        }

        void PlayerDeath()
        {
            Time.timeScale = 0f;
            SceneController._instance.GameOverScreen();
        }
        
        public void Hurt()
        {
            health--;
            anim.SetFloat("ShakeTime", 0.3f);
            sound.Play();
        }
    }
}
