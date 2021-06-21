using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Managers
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController Instance;

        public GameObject splashScreenDisplay;
        public GameObject titleScreenDisplay;
        public Text titleScreenText;
        public GameObject mainMenuScreen;
        public GameObject optionsScreenDisplay;
        public GameObject settings;
        public GameObject credits;
        private Scene _currentScene;

        [SerializeField] private float _splashScreenDelay;

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
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                titleScreenText.enabled = false;
                mainMenuScreen.SetActive(true);
            }
        }

        public void LoadLevel(Object scene)
        {
            SceneManager.LoadScene(scene.name);
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