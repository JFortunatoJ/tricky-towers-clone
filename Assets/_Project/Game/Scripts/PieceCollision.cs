using System;
using System.Collections;
using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    public class PieceCollision : MonoBehaviour
    {
        private Piece _piece;

        private Collider2D[] _colliders;

        private void Awake()
        {
            _piece = GetComponent<Piece>();
            _colliders = GetComponentsInChildren<Collider2D>();
        }

        private void Start()
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].isTrigger = true;
            }
        }

        private void FixedUpdate()
        {
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_piece.IsPlaced) return;

            _piece.IsPlaced = true;

            //Impede que um collider fique em cima de outro
            for (int i = 0; i < _colliders.Length; i++)
            {
                ColliderDistance2D distance = _colliders[i].Distance(col);
                if (distance.isOverlapped)
                {
                    _piece.transform.position += (Vector3)(distance.pointB - distance.pointA);
                }
            }

            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].isTrigger = false;
            }

            _piece._rigidbody.bodyType = RigidbodyType2D.Dynamic;

            _piece.OnCollideWithPiece?.Invoke();
            _piece.OnCollideWithPiece = null;
        }

        private bool IsMyCollider(Collider2D col)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i].Equals(col)) return true;
            }

            return false;
        }

        private IEnumerator HandlePieceCollisionCoroutine(Collision2D collision)
        {
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            _piece.IsPlaced = true;

            for (int i = 0; i < 3; i++)
            {
                yield return null;
            }

            _piece.ReturnToPreviousPosition();


            _piece._rigidbody.bodyType = RigidbodyType2D.Dynamic;
            collision.rigidbody.constraints = RigidbodyConstraints2D.None;

            _piece.OnCollideWithPiece?.Invoke();
            _piece.OnCollideWithPiece = null;
        }
    }
}