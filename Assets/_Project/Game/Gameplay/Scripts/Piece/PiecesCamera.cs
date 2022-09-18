using Blazewing;
using DG.Tweening;
using MiniclipTest.Game.Events;
using UnityEngine;

namespace MiniclipTest.Game
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
            DataEvent.Register<OnTowerHeightChanged>(OnTowerHeightChanged);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<OnPiecePlacedEvent>(OnPiecePlaced);
            DataEvent.Unregister<OnTowerHeightChanged>(OnTowerHeightChanged);
        }
        
        private void OnTowerHeightChanged(OnTowerHeightChanged eventData)
        {
            if(eventData.towerOwnerId != _playerId) return;
            
            transform.DOMoveY(eventData.towerHeight + cameraOffsetY, .5f);
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
        private string _playerId;
        [Space]
        [SerializeField]
        private float _defaultDuration = .2f;
        [SerializeField]
        private float _defaultStrength = .2f;
        [SerializeField]
        private int _defaultVibrato = 50;
        [Space]
        [SerializeField]
        private float cameraOffsetY = 10f;
        
        private Camera _camera;
    }
}