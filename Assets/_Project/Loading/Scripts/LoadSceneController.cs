using MiniclipTrick.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTrick.Load
{
    public class LoadSceneController : MonoBehaviour
    {
        private static readonly string SCENE_NAME = "Loading";
        private static string _sceneToLoad;
        
        public static void LoadScene(string sceneToLoad)
        {
            _sceneToLoad = sceneToLoad;
            SceneController.LoadScene(SCENE_NAME, LoadSceneMode.Single);
        }

        private void Start()
        {
            Time.timeScale = 1;
            SceneController.LoadScene(_sceneToLoad, LoadSceneMode.Single);
        }
    }
}