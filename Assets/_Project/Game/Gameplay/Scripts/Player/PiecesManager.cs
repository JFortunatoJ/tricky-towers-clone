using System;
using System.Collections.Generic;
using Blazewing;
using DG.Tweening;
using MiniclipTest.Game.Events;
using MiniclipTest.Game.Piece;
using MiniclipTest.Game.Scriptables;
using MiniclipTest.Utility;
using UnityEngine;

namespace MiniclipTest.Game.Player
{
    public class PiecesManager : MonoBehaviour
    {
        private void OnEnable()
        {
            DataEvent.Register<OnTowerHeightChanged>(OnTowerHeightChanged);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<OnTowerHeightChanged>(OnTowerHeightChanged);
        }

        public void Init(string playerId, Action<PieceController> onPiecePlaced, Action<PieceController> onPieceLost)
        {
            _playerId = playerId;
            
            _onPiecePlaced = onPiecePlaced;
            _onPieceLost = onPieceLost;

            CanSpawn = true;
            
            SetupPiecesPool();
        }

        private void FixedUpdate()
        {
            if(_currentPiece is null || _currentPiece.IsPlaced || _currentPiece.IsLost) return;

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
            CurrentPiece.Show();
        }
        
        public void StopSpawn()
        {
            CanSpawn = false;

            if (_currentPiece is not null && !_currentPiece.IsPlaced)
            {
                _currentPiece.DestroyPiece();
                _currentPiece = null;
            }
        }

        public void SetPiecePlacedParent(PieceController piece)
        {
            piece.transform.SetParent(_placedPiecesHolder);
        }

        private void InstantiateNewPiece(PieceController piecePrefab)
        {
            PieceController newPiece = Instantiate(piecePrefab, _pieceSpawnPoint.position, Quaternion.identity, _piecesHolder);
            newPiece.Initialize(_onPiecePlaced, _onPieceLost);
            newPiece.gameObject.SetActive(false);
            _piecesPool.Add(newPiece);
        }
        
        private void OnTowerHeightChanged(OnTowerHeightChanged eventData)
        {
            if(eventData.towerOwnerId != _playerId) return;
            
            _pieceSpawnPoint.DOMoveY(eventData.towerHeight + _spawnOffset, 0f);
        }
        
        private PieceController GetPieceToSpawn()
        {
            PieceController newPiece = _piecesPool.PopRandomItem();

            if (_piecesPool.Count <= 1)
            {
                SetupPiecesPool();
            }
            
            return newPiece;
        }
        
        [SerializeField]
        private PiecesContainerScriptable _piecesContainer;
        [Space]
        [SerializeField]
        private Transform _pieceSpawnPoint;
        [SerializeField]
        private Transform _piecesHolder;
        [SerializeField]
        private Transform _placedPiecesHolder;

        private string _playerId;
        private List<PieceController> _piecesPool;
        private List<PieceController> _placedPieces;
        private PieceController _currentPiece;
        private PlayerController _playerController;
        private Action<PieceController> _onPiecePlaced;
        private Action<PieceController> _onPieceLost;
        
        private readonly float _spawnOffset = 25.2f;

        public PieceController CurrentPiece
        {
            get => _currentPiece;
            private set => _currentPiece = value;
        }
        
        public int PiecesLost { get; set; }
        
        public bool CanSpawn { get; set; }
    }
}