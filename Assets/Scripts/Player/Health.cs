using Managers;
using UnityEngine;
using HUD;

namespace PlayerNS
{
    public class Health : MonoBehaviour
    {
        public static int health = 3;
        public Animator anim;
        public AudioSource sound;
        public float iTime;

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

            if (iTime>=0)
            {
                iTime -= Time.deltaTime;
            }
        }

        void PlayerDeath()
        {
            Time.timeScale = 0f;
            FindObjectOfType<HUD.HUD>().GameOverScreen();
        }
        
        public void Hurt()
        {
            if (iTime<=0)
            {
                health--;
                anim.SetFloat("ShakeTime", 0.3f);
                sound.Play();
                iTime = .8f;
            }
            
        }
    }
}
