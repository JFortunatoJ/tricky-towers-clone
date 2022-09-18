using System;
using UnityEngine;

namespace MiniclipTest.Game.Piece
{
    public class PieceCollision : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _puffParticles;
        
        private PieceController _thisPiece;
        private Collider2D[] _colliders;

        private static readonly string TowerPieceTag = "TowerPiece";

        public void Initialize(PieceController controller)
        {
            _thisPiece = controller;
            _colliders = GetComponentsInChildren<Collider2D>();

            SetCollidersTriggerStatus(true);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(TowerPieceTag)) return;
            
            if (_thisPiece.IsPlaced || _thisPiece.IsLost || !other.attachedRigidbody.TryGetComponent(out TowerStructure structure)) return;

            AvoidOverlap(other);
            SetCollidersTriggerStatus(false);

            _thisPiece.OnCollideWithOtherPiece();
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
                    _thisPiece.transform.position += (Vector3)(distance.pointB - distance.pointA);
                    PlayPuffParticles(distance.pointA);
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

        private void PlayPuffParticles(Vector3 position)
        {
            _puffParticles.transform.position = position;
            _puffParticles.Play();
        }
    }
}