using System;
using Unity.Collections;
using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PieceMovement))]
    [RequireComponent(typeof(PieceCollision))]
    public class PieceController : TowerStructure
    {
        [SerializeField]
        private Vector2 centerOfMass;
        [SerializeField]
        [ReadOnly]
        private bool _isPlaced;
        
        public PieceMovement Movement { get; private set; }
        public PieceCollision Collision { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        
        public bool IsPlaced
        {
            get => _isPlaced; 
            private set => _isPlaced = value;
        }

        public Action OnPlacePiece;
        
        public void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Movement = GetComponent<PieceMovement>();
            Collision = GetComponent<PieceCollision>();
            
            Rigidbody.WakeUp();
            Rigidbody.centerOfMass = centerOfMass;
            
            Movement.Initialize();
            Collision.Initialize(this);
            
            Collision.OnCollide += PlacePiece;
        }

        public void PlacePiece()
        {
            IsPlaced = true;
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;

            OnPlacePiece?.Invoke();
        }
    }
}