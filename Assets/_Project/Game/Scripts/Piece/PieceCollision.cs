using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    public class PieceCollision : MonoBehaviour
    {
        private PieceController _piece;
        private Collider2D[] _colliders;

        public Action OnCollisionFirstTime;

        public void Initialize(PieceController controller)
        {
            _piece = controller;
            _colliders = GetComponentsInChildren<Collider2D>();

            SetCollidersTriggerStatus(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            print($"Peça: {gameObject.name} colidiu com {other.gameObject.name}");
            
            if (_piece.IsPlaced) return;

            AvoidOverlap(other);
            SetCollidersTriggerStatus(false);
            
            OnCollisionFirstTime?.Invoke();
        }

        /// <summary>
        /// Impede que um collider fique em cima de outro
        /// </summary>
        /// <param name="col"></param>
        private void AvoidOverlap(Collider2D col)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                ColliderDistance2D distance = _colliders[i].Distance(col);
                if (distance.isOverlapped)
                {
                    _piece.transform.position += (Vector3)(distance.pointB - distance.pointA);
                }
            }
        }

        private void SetCollidersTriggerStatus(bool status)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].isTrigger = status;
            }
        }
    }
}