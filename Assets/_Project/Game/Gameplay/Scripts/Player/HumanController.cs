using Blazewing.DataEvent;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Piece;
using UnityEngine;

namespace MiniclipTrick.Game.Player
{
    [RequireComponent(typeof(PlayerInputController))]
    public class HumanController : PlayerController
    {
        private void Start()
        {
            _input = GetComponent<PlayerInputController>();
            
            _input.Init();
            _input.onRotateInput += OnRotateInput;
            _input.onMoveHorizontallyInput += OnMovePieceHorizontally;
        }
        
        private void OnRotateInput()
        {
            if(!CanPlay()) return;
            
            _piecesManager.CurrentPiece.Movement.Rotate();
        }
        
        private void OnMovePieceHorizontally(float direction)
        {
            if(!CanPlay()) return;
            
            _piecesManager.CurrentPiece.Movement.HorizontalStep((int)direction);
        }

        protected override void OnPiecePlaced(PieceController piece)
        {
            base.OnPiecePlaced(piece);
            DataEvent.Notify(new OnPiecePlacedEvent(piece));
        }

        protected override void OnPieceLost(PieceController piece)
        {
            base.OnPieceLost(piece);
            DataEvent.Notify(new OnPieceLostEvent(piece));
        }

        private PlayerInputController _input;
    }
}