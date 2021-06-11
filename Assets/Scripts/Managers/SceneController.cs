using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Managers
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController Instance;
    
        public Object titleScreen;
        public Object optionsScreen;
        public GameObject settings;
        public GameObject credits;
        private Scene _currentScene;
    
        [SerializeField] private float _splashScreenDelay;
        private bool _isOptionsReferenced;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    
        private void Start()
        {
            StartCoroutine(SplashScreenDelay());
        }
    
        private IEnumerator SplashScreenDelay()
        {
            yield return new WaitForSeconds(_splashScreenDelay);
            LoadLevel(titleScreen);
        }
    
        public void LoadLevel(Object scene)
        {
            SceneManager.LoadScene(scene.name);
        }
    
        // TODO - work in progress
        public void PanelReveal(GameObject panel)
        {
            if (panel == settings)
            {
                settings.SetActive(true);
                credits.SetActive(false);
            } 
            else 
            {
                credits.SetActive(true);
                settings.SetActive(false);
            }
        }
    
        private void Update()
        {
            _currentScene = SceneManager.GetActiveScene();
            if (_currentScene.name == "Option Screen" && !_isOptionsReferenced)
            {
                _isOptionsReferenced = true;
                settings = GameObject.Find("Settings Panel");
                credits = GameObject.Find("Credits Panel");
                credits.SetActive(false);
            }
        }
    
        public void ExitGame()
        {
            Application.Quit();
        }
        
    }
}
