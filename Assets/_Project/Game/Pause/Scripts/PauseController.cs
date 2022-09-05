using Blazewing.DataEvent;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Utility;
using UnityEngine;

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
        }

        private void OnExit()
        {
        }
    }
}