using Blazewing;
using MiniclipTest.Game.Events;
using MiniclipTest.Load;
using MiniclipTest.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTest.Game.Pause
{
    [RequireComponent(typeof(PauseView))]
    public class PauseController : BaseController
    {
        private PauseView _view;

        public static readonly string SCENE_NAME = "PauseScreen";

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
            Hide(SCENE_NAME);
            
            DataEvent.Notify(new OnPauseEvent(false));
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