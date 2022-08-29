using System;
using System.Collections;
using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    public class PieceCollision : MonoBehaviour
    {
        private Piece _piece;

        private void Awake()
        {
            _piece = GetComponent<Piece>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("DestroyPiece"))
            {
                Destroy(gameObject);
                return;
            }
            
            if (_piece.IsPlaced) return;

            ContactPoint2D contact = collision.GetContact(0);

            if (!(contact.normal.y >= .15f)) return;

            StartCoroutine(HandlePieceCollisionCoroutine(collision));
        }
        
        private IEnumerator HandlePieceCollisionCoroutine(Collision2D collision)
        {
            collision.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            _piece.IsPlaced = true;

            for (int i = 0; i < 2; i++)
            {
                yield return null;
            }

            collision.rigidbody.constraints = RigidbodyConstraints2D.None;
            
            //_rigidbody.position = _previousPosition;

            _piece.OnCollideWithPiece?.Invoke();
            _piece.OnCollideWithPiece = null;
        }
    }
}