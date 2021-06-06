using UnityEngine;

namespace Utility
{
    public class OnOffCycle : MonoBehaviour
    {
        public GameObject gameObjectToToggle;
        [SerializeField] private int timeOfActivity;
        private float _timer;
        private bool _isActive;

        // Update is called once per frame //
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > timeOfActivity && !_isActive)
            {
                _isActive = true;
                gameObjectToToggle.SetActive(true);
                _timer = 0;
            }
            else if (_timer > timeOfActivity && _isActive)
            {
                _isActive = false;
                gameObjectToToggle.SetActive(false);
                _timer = 0;
            }
        }
    }
}