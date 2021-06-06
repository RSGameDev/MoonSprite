using PlayerNS;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class HUD : MonoBehaviour
    {
        public GameObject playerHealth;
        private TextMeshProUGUI _playerHealthText;

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
        }
    }
}