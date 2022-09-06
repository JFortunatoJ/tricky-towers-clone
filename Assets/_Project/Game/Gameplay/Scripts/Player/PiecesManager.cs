using System;
using System.Collections.Generic;
using MiniclipTrick.Game.Piece;
using MiniclipTrick.Game.Scriptables;
using MiniclipTrick.Utility;
using UnityEngine;

namespace MiniclipTrick.Game.Player
{
    public class PiecesManager : MonoBehaviour
    {
        [SerializeField]
        private PiecesContainerScriptable _piecesContainer;
        [Space]
        [SerializeField]
        private Transform _pieceSpawnPoint;
        [SerializeField]
        private Transform _placedPiecesHolder;
        [Space]
        [SerializeField]
        private Vector2 _spawnerRangePosition;

        private List<PieceController> _piecesPool;
        private List<PieceController> _placedPieces;
        private PieceController _currentPiece;
        private Vector3 _currentSpawnPosition;
        private PlayerController _playerController;

        public PieceController CurrentPiece
        {
            get => _currentPiece;
            private set => _currentPiece = value;
        }
        public int PiecesLost { get; set; }
        
        public bool CanSpawn { get; set; }

        private Action<PieceController> _onPiecePlaced;
        private Action<PieceController> _onPieceLost;

        public void Init(Action<PieceController> onPiecePlaced, Action<PieceController> onPieceLost)
        {
            _onPiecePlaced = onPiecePlaced;
            _onPieceLost = onPieceLost;
            
            _currentSpawnPosition = _pieceSpawnPoint.localPosition;

            CanSpawn = true;
            
            SetupPiecesPool();
        }

        private void FixedUpdate()
        {
            if(_currentPiece == null || _currentPiece.IsPlaced || _currentPiece.IsLost) return;

            _currentPiece.Movement.MoveDownwards();
        }

        public void SetupPiecesPool()
        {
            _piecesPool ??= new List<PieceController>();
            
            int piecesCount = _piecesContainer.Count;
            
            for (int i = 0; i < piecesCount; i++)
            {
                InstantiateNewPiece(_piecesContainer.GetPieceByIndex(i));
            }
        }
        
        public void SpawnPiece()
        {
            if(!CanSpawn) return;
            
            CurrentPiece = GetPieceToSpawn();
            CurrentPiece.gameObject.SetActive(true);
        }
        
        public void StopSpawn()
        {
            CanSpawn = false;

            if (!_currentPiece.IsPlaced)
            {
                _currentPiece.DestroyPiece();
            }
        }

        private void InstantiateNewPiece(PieceController piecePrefab)
        {
            PieceController newPiece = Instantiate(piecePrefab, _pieceSpawnPoint);
            newPiece.Initialize(_onPiecePlaced, _onPieceLost, null);
            newPiece.gameObject.SetActive(false);
            _piecesPool.Add(newPiece);
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
    }
}