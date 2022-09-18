using Blazewing;
using MiniclipTest.Load;
using MiniclipTest.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniclipTest.GameOver
{
    public class GameOverController : BaseController
    {
        [SerializeField]
        protected GameOverView _view;

        protected virtual void Start()
        {
            _view.OnPlayAgainButtonClick += PlayAgain;
            _view.OnMainMenuButtonClick += ReturnToMainMenu;

            _view.Show();
        }

        protected virtual void PlayAgain()
        {
            string sceneName = DataController.Exist<GameMode>() ? DataController.Get<GameMode>().sceneName : "StartMenu";
            LoadSceneController.LoadScene(sceneName);
        }

        protected virtual void ReturnToMainMenu()
        {
            SceneController.LoadScene("StartMenu", LoadSceneMode.Single, () => Time.timeScale = 1);
        }
    }
}