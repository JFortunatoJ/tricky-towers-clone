using System;
using UnityEngine.SceneManagement;

namespace MiniclipTrick.Utility
{
    public class SceneController
    {
        public static void LoadScene(string nextScene, LoadSceneMode mode = LoadSceneMode.Additive,
            Action callback = null, bool useFade = true, bool loadExistingScene = false)
        {
            var scene = SceneManager.GetSceneByName(nextScene);
            if (!scene.isLoaded || loadExistingScene)
            {
                if (useFade)
                {
                    FadeController.Instance.LoadScene(nextScene, mode, callback);
                }
                else
                {
                    SceneManager.LoadSceneAsync(nextScene, mode);
                    callback?.Invoke();
                }
            }
        }

        public static void UnloadScene(string sceneToUnload)
        {
            var scene = SceneManager.GetSceneByName(sceneToUnload);
            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneToUnload);
            }
        }
    }
}