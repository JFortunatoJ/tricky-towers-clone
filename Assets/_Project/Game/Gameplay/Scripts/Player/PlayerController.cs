using Blazewing.DataEvent;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Piece;
using UnityEngine;

namespace MiniclipTrick.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public string playerId;
        [SerializeField]
        private PiecesManager _piecesManager;
        [SerializeField]
        private EndLineController _endLine;

        private int _maxPiecesLostAllowed;
        private bool _isPaused;
        private bool _gameOver;

        private void OnEnable()
        {
            DataEvent.Register<OnPauseEvent>(OnPauseEvent);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<OnPauseEvent>(OnPauseEvent);
        }

        private void OnPauseEvent(OnPauseEvent eventData)
        {
            _isPaused = eventData.isPaused;
        }

        public void Init(int maxPiecesLostAllowed)
        {
            _maxPiecesLostAllowed = maxPiecesLostAllowed;
            
            _endLine.OnCountdownStarted += OnCountdownStarted;
            _endLine.OnCountdownCanceled += OnCountdownCanceled;
            _endLine.OnCountdownComplete += OnCountdownComplete;

            _piecesManager.Init(OnPiecePlaced, OnPieceLost);
            _piecesManager.SpawnPiece();
        }

        private void Update()
        {
            if(_isPaused || _gameOver) return;
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                _piecesManager.SpawnPiece();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _piecesManager.CurrentPiece.Movement.Rotate();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _piecesManager.CurrentPiece.Movement.MoveHorizontally(-1);
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _piecesManager.CurrentPiece.Movement.MoveHorizontally(1);
            }

            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _piecesManager.CurrentPiece.Movement.BoostSpeed();
            }
            
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                _piecesManager.CurrentPiece.Movement.ResetSpeed();
            }
        }

        private void OnPiecePlaced(PieceController piece)
        {
            if (!_endLine.pieceDetectorRay.CheckPiece())
            {
                _piecesManager.SpawnPiece();
            }
        }

        private void OnPieceLost(PieceController piece)
        {
            _piecesManager.PiecesLost++;

            if (_piecesManager.PiecesLost < _maxPiecesLostAllowed)
            {
                if (!piece.IsPlaced)
                {
                    _piecesManager.SpawnPiece();
                }
                
                return;
            }
            
            DataEvent.Notify(new OnPlayerGameOverEvent(playerId, false));
        }
        
        private void OnCountdownStarted()
        {
            _piecesManager.CanSpawn = false;
        }
        
        private void OnCountdownCanceled()
        {
            _piecesManager.CanSpawn = true;
            _piecesManager.SpawnPiece();
        }
        
        private void OnCountdownComplete()
        {
            DataEvent.Notify(new OnPlayerGameOverEvent(playerId, true));
        }

        public void GameOver()
        {
            _gameOver = true;
            _piecesManager.StopSpawn();
        }
    }
}