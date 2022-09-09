using Blazewing;
using DG.Tweening;
using UnityEngine;
using MiniclipTrick.Game.Events;

namespace MiniclipTrick.Game
{
    public class PiecesCamera : MonoBehaviour, IShakeable
    {
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable()
        {
            DataEvent.Register<OnPiecePlacedEvent>(OnPiecePlaced);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<OnPiecePlacedEvent>(OnPiecePlaced);
        }

        private void OnPiecePlaced(OnPiecePlacedEvent eventData)
        {
            Shake(_defaultDuration, _defaultStrength, _defaultVibrato);
        }

        public void Shake(float duration, float strength, int vibrato)
        {
            _camera.DOShakePosition(.2f, .2f, 50);
        }
        
        [SerializeField]
        private float _defaultDuration = .2f;
        [SerializeField]
        private float _defaultStrength = .2f;
        [SerializeField]
        private int _defaultVibrato = 50;
        
        private Camera _camera;
    }
}