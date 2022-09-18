using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniclipTest.StartMenu
{
    public class StartMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _onePlayerButton;
        [SerializeField]
        private Button _playerVsAiButton;

        public Action onOnePlayerButtonClick;
        public Action onPlayerVsAiButtonClick;

        private void Start()
        {
            _onePlayerButton.onClick.AddListener(() => onOnePlayerButtonClick?.Invoke());
            _playerVsAiButton.onClick.AddListener(() => onPlayerVsAiButtonClick?.Invoke());
        }
    }
}