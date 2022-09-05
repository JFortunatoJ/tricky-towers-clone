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
        }
        
        private void OnPlayerVsAiButtonClick()
        {
            
        }
    }
}