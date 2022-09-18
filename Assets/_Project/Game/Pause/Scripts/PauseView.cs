using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniclipTest.Game.Pause
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _exitButton;

        public Action OnContinueButtonClick;
        public Action OnRestartButtonClick;
        public Action OnExitButtonClick;

        private void Start()
        {
            _continueButton.onClick.AddListener(() => OnContinueButtonClick?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartButtonClick?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonClick?.Invoke());
        }
    }
}