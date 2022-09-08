using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace MiniclipTrick.Game.Piece
{
    public class PieceController : TowerStructure
    {
        [SerializeField]
        private PieceMovement _movement;
        [SerializeField]
        private PieceCollision _collision;
        [Space]
        [SerializeField]
        [ReadOnly]
        private bool _isPlaced;
        [SerializeField]
        [ReadOnly] private bool _isLost;

        public PieceMovement Movement => _movement;
        public PieceCollision Collision => _collision;

        public Action<PieceController> OnPiecePlaced;
        public Action<PieceController> OnPieceLost;
        
        public bool IsPlaced
        {
            get => _isPlaced; 
            private set => _isPlaced = value;
        }

        public bool IsLost
        {
            get => _isLost;
            private set => _isLost = value;
        }

        public void Initialize( Action<PieceController> onPiecePlaced, Action<PieceController> onPieceLost)
        {
            Assert.IsNotNull(_movement);
            Assert.IsNotNull(_collision);
            
            Movement.Initialize(this);
            Collision.Initialize(this);

            OnPieceLost = onPieceLost;
            OnPiecePlaced = onPiecePlaced;

            IsPlaced = false;
            IsLost = false;
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
    }
}