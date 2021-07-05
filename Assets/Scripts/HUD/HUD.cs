using PlayerNS;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseScreen;
        public GameObject playerHealth;
        private TextMeshProUGUI _playerHealthText;
        private bool _pauseToggle;

        // Start is called before the first frame update
        private void Awake()
        {
            _playerHealthText = playerHealth.GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _playerHealthText.SetText(Health.health.ToString());
        }

        // Update is called once per frame
        private void Update()
        {
            _playerHealthText.SetText(Health.health.ToString());
            
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