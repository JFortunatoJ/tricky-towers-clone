using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseController : MonoBehaviour
{
    public static bool Show(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool ignoreDuplicatedScenes = false)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            //Avoid duplicated scenes
            if (mode == LoadSceneMode.Additive && !ignoreDuplicatedScenes)
            {
                var scene = SceneManager.GetSceneByName(sceneName);

                if (scene.isLoaded) return true;
            }

            SceneManager.LoadSceneAsync(sceneName, mode);
        }

        return false;
    }

    public static void Hide(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.UnloadSceneAsync(sceneName);
    }
}
