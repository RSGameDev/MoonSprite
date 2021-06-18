using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace PipLib.Stage
{
    public static class SceneControls
    {
        public static void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public static void PreviousLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public static void RandomLevel(int? min = 0,int? max = 0)
        {
            
            SceneManager.LoadScene(Random.Range(min??0,max?? SceneManager.sceneCount));
        }

        public static void To(int index)
        {
            SceneManager.LoadScene(index);
        }

        public static void Restart()
        {
            Debug.Log("SCENE RESTARTED");
            Debug.Log("Reminder: Static variables don't reset on Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public static int Read()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}