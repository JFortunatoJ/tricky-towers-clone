using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseController : MonoBehaviour
{
    public static bool Show(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive, bool ignoreDuplicatedScenes = false)
    {
        if (string.IsNullOrEmpty(sceneName)) return false;
        
        //Avoid duplicated scenes
        if (mode == LoadSceneMode.Additive && !ignoreDuplicatedScenes)
        {
            var scene = SceneManager.GetSceneByName(sceneName);

            if (scene.isLoaded) return true;
        }

        SceneManager.LoadSceneAsync(sceneName, mode);

        return false;
    }

    public static void Hide(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
