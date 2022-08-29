using System;
using System.Collections.Generic;
using MiniclipTrick.Utility;
using UnityEngine;
using Zenject;

namespace MiniclipTrick.Game.Piece
{
    public class PiecesController : MonoBehaviour
    {
        [SerializeField]
        private Piece[] _piecesPrefabs;
        [Space]
        [SerializeField]
        private Transform _pieceSpawnPoint;
        [SerializeField]
        private Transform _placedPiecesHolder;
        [Space]
        [SerializeField]
        private Vector2 _spawnerRangePosition;

        private List<Piece> _piecesPool;
        private Piece _currentPiece;
        private Vector3 _currentSpawnPosition;
        private PiecesCamera _piecesCamera;
        
        public Piece CurrentPiece
        {
            get => _currentPiece;
            private set => _currentPiece = value;
        }

        [Inject]
        public void Construct(PiecesCamera piecesCamera)
        {
            _piecesCamera = piecesCamera;
        }

        private void Start()
        {
            _currentSpawnPosition = _pieceSpawnPoint.localPosition;
        }

        private void FixedUpdate()
        {
            if(_currentPiece == null || _currentPiece.IsPlaced) return;

            _currentPiece.MoveDownwards();
        }

        public void SetupPiecesPool()
        {
            _piecesPool ??= new List<Piece>();
            
            int piecesCount = _piecesPrefabs.Length;
            
            for (int i = 0; i < piecesCount; i++)
            {
                Piece newPiece = Instantiate(_piecesPrefabs[i], _pieceSpawnPoint);
                newPiece.OnCollideWithPiece += OnPiecePlaced;
                newPiece.gameObject.SetActive(false);
                _piecesPool.Add(newPiece);
            }
        }

        public void SpawnPiece()
        {
            if (_currentPiece != null)
            {
                _currentPiece.transform.SetParent(_placedPiecesHolder);
            }
            
            CurrentPiece = _piecesPool.PopRandomItem();

            if (_piecesPool.Count <= 1)
            {
                SetupPiecesPool();
            }

            CurrentPiece.gameObject.SetActive(true);
        }

        private void OnPiecePlaced()
        {
            _piecesCamera.ShakeCamera();
            SpawnPiece();
        }
    }
}