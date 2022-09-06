using MiniclipTrick.Utility;
using UnityEngine;

namespace MiniclipTrick.GameOver
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField]
        protected GameOverView _view;

        public static void Show(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneController.LoadScene(sceneName, useFade: false);
            }
        }

        public static void Hide(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneController.UnloadScene(sceneName);
            }
        }

        protected virtual void Start()
        {
            _view.OnPlayAgainButtonClick += PlayAgain;
            _view.OnMainMenuButtonClick += ReturnToMainMenu;

            _view.Show();
        }

        protected virtual void PlayAgain()
        {
            SceneController.LoadScene("");
        }

        protected virtual void ReturnToMainMenu()
        {
            SceneController.LoadScene("StartMenu");
        }
    }
}