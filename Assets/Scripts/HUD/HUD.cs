using PlayerNS;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class HUD : MonoBehaviour
    { 
        /// <summary>
        /// whjdjbjkhsbfcjkhsdbcvjhbcsdcsdcjb
        /// </summary>
        [SerializeField] private GameObject _pauseScreen;
        public GameObject playerHealth;
        public TextMeshProUGUI playerHealthText;
        private bool _pauseToggle;

        // Start is called before the first frame update
        private void Awake()
        {
            playerHealthText = playerHealth.GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            playerHealthText.SetText(Health.health.ToString());
        }

        // Update is called once per frame
        private void Update()
        {
            playerHealthText.SetText(Health.health.ToString());
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseToggle();
            }
        }
        
        public void PauseToggle()
        {
            if (!_pauseToggle)
            {
                _pauseScreen.SetActive(true);
                Time.timeScale = 0f;
                _pauseToggle = true;
            }
            else
            {
                _pauseScreen.SetActive(false);
                Time.timeScale = 1f;
                _pauseToggle = false;
            }
        }
    
        void ResumeButton()
        {
            PauseToggle();
        }
    }
}