using System;
using DG.Tweening;

namespace MiniclipTest.Game.Piece
{
    public class PieceController : TowerStructure
    {
        public void Initialize(Action<PieceController> onPiecePlaced, Action<PieceController> onPieceLost)
        {
            Movement = GetComponent<PieceMovement>();
            Collision = GetComponent<PieceCollision>();
            
            Movement.Initialize(this);
            Collision.Initialize(this);

            OnPieceLost = onPieceLost;
            OnPiecePlaced = onPiecePlaced;

            IsPlaced = false;
            IsLost = false;
        }

        public void Show()
        {
            Movement.CanMove = false;
            transform.DOScale(1, .35f).SetEase(Ease.OutBack).onComplete = () => Movement.CanMove = true;
        }

        public void OnCollideWithOtherPiece()
        {
            IsPlaced = true;
            Movement.EnablePhysics();

            OnPiecePlaced?.Invoke(this);
        }
        
        public void OnPieceFallAtWater()
        {
            IsLost = true;
            Movement.EnablePhysics();
            OnPieceLost?.Invoke(this);
        }

        public void DestroyPiece()
        {
            Destroy(gameObject);
        }
        
        public PieceMovement Movement { get; private set; }
        public PieceCollision Collision { get; private set; }

        public Action<PieceController> OnPiecePlaced;
        public Action<PieceController> OnPieceLost;
        
        public bool IsPlaced { get; private set; }

        public bool IsLost { get; private set; }
    }
}