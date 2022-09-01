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
        private PieceController[] _piecesPrefabs;
        [Space]
        [SerializeField]
        private Transform _pieceSpawnPoint;
        [SerializeField]
        private Transform _placedPiecesHolder;
        [Space]
        [SerializeField]
        private Vector2 _spawnerRangePosition;

        private List<PieceController> _piecesPool;
        private PieceController _currentPiece;
        private Vector3 _currentSpawnPosition;
        private PiecesCamera _piecesCamera;
        
        public PieceController CurrentPiece
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

            _currentPiece.Movement.MoveDownwards();
        }

        public void SetupPiecesPool()
        {
            _piecesPool ??= new List<PieceController>();
            
            int piecesCount = _piecesPrefabs.Length;
            
            for (int i = 0; i < piecesCount; i++)
            {
                PieceController newPiece = Instantiate(_piecesPrefabs[i], _pieceSpawnPoint);
                newPiece.OnPlacePiece += OnPiecePlaced;
                newPiece.Initialize();
                newPiece.gameObject.SetActive(false);
                _piecesPool.Add(newPiece);
            }
        }

        public void SpawnPiece()
        {
            CurrentPiece = GetPieceToSpawn();
            CurrentPiece.gameObject.SetActive(true);
        }

        private PieceController GetPieceToSpawn()
        {
            if (_currentPiece != null)
            {
                _currentPiece.transform.SetParent(_placedPiecesHolder);
            }
            
            PieceController newPiece = _piecesPool.PopRandomItem();

            if (_piecesPool.Count <= 1)
            {
                SetupPiecesPool();
            }
            
            return newPiece;
        }

        private void OnPiecePlaced()
        {
            _piecesCamera.ShakeCamera();
            SpawnPiece();
        }
    }
}