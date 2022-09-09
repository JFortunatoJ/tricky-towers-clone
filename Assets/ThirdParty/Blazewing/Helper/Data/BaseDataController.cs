using Blazewing;
using UnityEngine.SceneManagement;

public abstract class BaseDataController<T> : BaseController where T : struct
{
    public static void Show(T data, string sceneName, LoadSceneMode mode, bool ignoreDuplicatedScenes = false)
    {
        DataController.Add(data);
        
        bool alreadyLoaded = Show(sceneName, mode, ignoreDuplicatedScenes);

        if (alreadyLoaded)
            FindObjectOfType<BaseDataController<T>>().Initialize(data);
    }

    void Start()
    {
        var data = DataController.Get<T>();
        Initialize(data);
    }

    /// <summary>
    /// Initialize is called on start
    /// </summary>
    /// <param name="data"></param>
    public abstract void Initialize(T data);
}