using Blazewing;
using MiniclipTrick.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTrick.StartMenu
{
    [RequireComponent(typeof(StartMenuView))]
    public class StartMenuController : MonoBehaviour
    {
        private StartMenuView _view;

        private void Awake()
        {
            _view = GetComponent<StartMenuView>();
        }

        private void Start()
        {
            _view.onOnePlayerButtonClick += OnOnePlayerButtonClick;
            _view.onPlayerVsAiButtonClick += OnPlayerVsAiButtonClick;
        }

        private void OnOnePlayerButtonClick()
        {
            SceneController.LoadScene("Gameplay", LoadSceneMode.Single);
            DataController.Add(new GameMode("Gameplay"));
        }
        
        private void OnPlayerVsAiButtonClick()
        {
            SceneController.LoadScene("Gameplay_PVE", LoadSceneMode.Single);
            DataController.Add(new GameMode("Gameplay_PVE"));
        }
    }
}