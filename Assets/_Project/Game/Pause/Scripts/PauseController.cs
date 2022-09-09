using Blazewing;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Load;
using MiniclipTrick.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTrick.Game.Pause
{
    [RequireComponent(typeof(PauseView))]
    public class PauseController : MonoBehaviour
    {
        private PauseView _view;

        private static readonly string SCENE_NAME = "PauseScreen";

        public static void Show()
        {
            Time.timeScale = 0;
            SceneController.LoadScene(SCENE_NAME, useFade: false);
        }

        public static void Hide()
        {
            Time.timeScale = 1;
            SceneController.UnloadScene(SCENE_NAME);
            DataEvent.Notify(new OnPauseEvent(false));
        }

        private void Awake()
        {
            _view = GetComponent<PauseView>();
        }

        private void Start()
        {
            _view.OnContinueButtonClick += OnContinue;
            _view.OnRestartButtonClick += OnRestart;
            _view.OnExitButtonClick += OnExit;
        }

        private void OnContinue()
        {
            Hide();
        }

        private void OnRestart()
        {
            string sceneName = DataController.Exist<GameMode>() ? DataController.Get<GameMode>().sceneName : "StartMenu";
            LoadSceneController.LoadScene(sceneName);
        }

        private void OnExit()
        {
            SceneController.LoadScene("StartMenu", LoadSceneMode.Single, callback:() => Time.timeScale = 1);
        }
    }
}