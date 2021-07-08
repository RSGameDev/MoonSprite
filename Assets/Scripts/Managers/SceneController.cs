using System;
using System.Collections;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController _instance;

        public GameObject splashScreenDisplay;
        public GameObject titleScreenDisplay;
        public Text titleScreenText;
        public GameObject mainMenuScreen;
        public GameObject optionsScreenDisplay;
        public GameObject settings;
        public GameObject credits;
        
        private bool _pauseToggle;
        private Scene _currentScene;
        public string preGameScene;
        public string firstLevel;
        public string secondLevel;
        public string firstCutScene;
        public string secondCutScene;
        public int sceneTransitionCount = 0;

        [SerializeField] private float _splashScreenDelay;

        private bool _isKeyToEnter;
        private bool _referenceInGameDisplays;
        private bool _isFirstTransitionCompleted;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _currentScene = SceneManager.GetActiveScene();
            titleScreenDisplay.SetActive(false);
            mainMenuScreen.SetActive(false);
            optionsScreenDisplay.SetActive(false);
            StartCoroutine(SplashScreenDelay());
        }

        private IEnumerator SplashScreenDelay()
        {
            yield return new WaitForSeconds(_splashScreenDelay);
            splashScreenDisplay.SetActive(false);
            titleScreenDisplay.SetActive(true);
            _isKeyToEnter = true;
        }

        private void Update()
        {
            if (_currentScene.name != preGameScene && !_referenceInGameDisplays)
            {
                
                InitCurrentScene();
               
               
            }

            if (_isKeyToEnter)
            {
                HitAnyKeyDisplayTransition(_currentScene);
            }

            // L for Level/LevelChange/LevelSkip - SCENE TRANSITION CODE
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    _currentScene = SceneManager.GetActiveScene();
            //    if (_currentScene.name == firstLevel.name && !_isFirstTransitionCompleted)
            //    {
            //        _isFirstTransitionCompleted = true;
            //        LevelTransition();
            //    }
            //}
        }
        
        public void LevelTransition()
        {
            switch (sceneTransitionCount)
            {
                case 0:
                    LoadLevel(firstCutScene);
                    break;
                case 1:
                    LoadLevel(secondCutScene);
                    break;
            }

            sceneTransitionCount++;
        }

        public void InitCurrentScene()
        {
            _currentScene = SceneManager.GetActiveScene();
            _referenceInGameDisplays = true;
        }

        

        private void HitAnyKeyDisplayTransition(Scene scene)
        {
            if (Input.anyKeyDown)
            {
                if (scene.name == preGameScene)
                {
                    _isKeyToEnter = false;
                    titleScreenText.enabled = false;
                    mainMenuScreen.SetActive(true);
                }
                else
                {
                    _isKeyToEnter = false;
                    
                    SceneManager.LoadScene(_currentScene.name);
                    _referenceInGameDisplays = false;
                    Time.timeScale = 1f;
                }
            }
        }

        public void LoadLevel(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void MenuScreenDisplay(GameObject gameObject)
        {
            if (gameObject == optionsScreenDisplay)
            {
                mainMenuScreen.SetActive(false);
                optionsScreenDisplay.SetActive(true);
            }

            if (gameObject == mainMenuScreen)
            {
                mainMenuScreen.SetActive(true);
                optionsScreenDisplay.SetActive(false);
            }
        }

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

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}