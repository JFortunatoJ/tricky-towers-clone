using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MiniclipTest.GameOver
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup _canvasGroup;
        [Space]
        [SerializeField]
        protected Button _playAgainButton;
        [SerializeField]
        protected Button _mainMenuButton;
        
        protected readonly float _animationDuration = .25f;

        public Action OnPlayAgainButtonClick;
        public Action OnMainMenuButtonClick;

        protected virtual void Start()
        {
            _playAgainButton.onClick.AddListener(() => OnPlayAgainButtonClick?.Invoke());
            _mainMenuButton.onClick.AddListener(() => OnMainMenuButtonClick?.Invoke());
        }

        public virtual void Show()
        {
            _canvasGroup.DOFade(1, _animationDuration).SetUpdate(true);
        }

        public virtual void Hide()
        {
            _canvasGroup.DOFade(0, _animationDuration).SetUpdate(true);
        }
    }
}