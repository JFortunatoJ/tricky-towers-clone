using Blazewing;
using MiniclipTest.Game.Events;
using MiniclipTest.Game.Piece;
using UnityEngine;

namespace MiniclipTest.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            DataEvent.Register<OnGameStartedEvent>(OnGameStarted);
            DataEvent.Register<OnPauseEvent>(OnPauseEvent);
        }

        protected virtual void OnDisable()
        {
            DataEvent.Unregister<OnGameStartedEvent>(OnGameStarted);
            DataEvent.Unregister<OnPauseEvent>(OnPauseEvent);
        }

        public virtual void Init(string playerId)
        {
            this.playerId = playerId;
            
            _finishLineLine.OnCountdownStarted += OnCountdownStarted;
            _finishLineLine.OnCountdownCanceled += OnCountdownCanceled;
            _finishLineLine.OnCountdownComplete += OnCountdownComplete;

            _piecesManager.Init(playerId, OnPiecePlaced, OnPieceLost);
            _towerHeightChecker.Init(playerId);
        }
        
        protected virtual void OnGameStarted(OnGameStartedEvent obj)
        {
            _piecesManager.SpawnPiece();
        }
        
        protected virtual void OnPauseEvent(OnPauseEvent eventData)
        {
            _isPaused = eventData.isPaused;
        }
        
        protected virtual bool CanPlay()
        {
            return !_isPaused && !_gameOver;
        }

        protected virtual void OnPiecePlaced(PieceController piece)
        {
            if(_gameOver) return;
            
            _piecesManager.SetPiecePlacedParent(piece);
            
            if (!_finishLineLine.pieceDetectorRay.CheckPiece())
            {
                _piecesManager.SpawnPiece();
            }
        }

        protected virtual void OnPieceLost(PieceController piece)
        {
            if(_gameOver) return;
            
            _piecesManager.PiecesLost++;

            if (GameManager.Instance.IsAgainstCPU)
            {
                if (!piece.IsPlaced)
                {
                    _piecesManager.SpawnPiece();
                }
                return;
            }
            
            if (_piecesManager.PiecesLost < GameSettings.Instance.PiecesLostToGameOver)
            {
                if (!piece.IsPlaced)
                {
                    _piecesManager.SpawnPiece();
                }

                return;
            }

            DataEvent.Notify(new OnPlayerGameOverEvent(playerId, false));
        }
        
        protected virtual void OnCountdownStarted()
        {
            _piecesManager.CanSpawn = false;
        }
        
        protected virtual void OnCountdownCanceled()
        {
            _piecesManager.CanSpawn = true;
            _piecesManager.SpawnPiece();
        }
        
        protected virtual void OnCountdownComplete()
        {
            DataEvent.Notify(new OnPlayerGameOverEvent(playerId, true));
        }

        public void GameOver()
        {
            _gameOver = true;
            _piecesManager.StopSpawn();
        }
        
        public string playerId;
        [Space]
        [SerializeField]
        protected PiecesManager _piecesManager;
        [SerializeField]
        protected FinishLineController _finishLineLine;
        [SerializeField]
        protected TowerHeightChecker _towerHeightChecker;

        protected bool _isPaused;
        protected bool _gameOver;
    }
}