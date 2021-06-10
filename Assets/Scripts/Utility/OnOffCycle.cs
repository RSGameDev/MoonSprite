using UnityEngine;

namespace Utility
{
    public class OnOffCycle : MonoBehaviour
    {
        public Color color;
        private SpriteRenderer sr;
        public float delay;
        public GameObject gameObjectToToggle;
        [SerializeField] private int timeOfActivity;
        private float _timer;
        private bool _isActive;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            _timer -= delay;
        }
        // Update is called once per frame //
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > timeOfActivity && !_isActive)
            {
                color.b = 0;
                color.g = 0;
                sr.color = color;
                _isActive = true;
                gameObjectToToggle.SetActive(true);
                _timer = 0;
            }
            else if (_timer > timeOfActivity && _isActive)
            {
                color.b = 1;
                color.g = 1;
                sr.color = color;
                _isActive = false;
                gameObjectToToggle.SetActive(false);
                _timer = 0;
            }
        }
    }
}