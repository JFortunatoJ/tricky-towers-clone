using Blazewing;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Piece;
using UnityEngine;
using Zenject;

namespace MiniclipTrick.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Inject]
        public void Construct(LevelSettings settings)
        {
            _settings = settings;
        }
        
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
            
            _endLine.OnCountdownStarted += OnCountdownStarted;
            _endLine.OnCountdownCanceled += OnCountdownCanceled;
            _endLine.OnCountdownComplete += OnCountdownComplete;

            _piecesManager.Init(OnPiecePlaced, OnPieceLost);
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
            
            if (!_endLine.pieceDetectorRay.CheckPiece())
            {
                _piecesManager.SpawnPiece();
            }
        }

        protected virtual void OnPieceLost(PieceController piece)
        {
            if(_gameOver) return;
            
            _piecesManager.PiecesLost++;

            if (_settings.againstCPU)
            {
                if (!piece.IsPlaced)
                {
                    _piecesManager.SpawnPiece();
                }
                return;
            }
            
            if (_piecesManager.PiecesLost < _settings.piecesLostToGameOver)
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
        [SerializeField]
        protected PiecesManager _piecesManager;
        [SerializeField]
        protected EndLineController _endLine;

        protected LevelSettings _settings;
        protected bool _isPaused;
        protected bool _gameOver;
    }
}