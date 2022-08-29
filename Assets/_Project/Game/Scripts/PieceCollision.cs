using System;
using System.Collections;
using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    public class PieceCollision : MonoBehaviour
    {
        private Piece _piece;
        private PolygonCollider2D _collider;
        [SerializeField]
        private Transform[] _collisionPoints;

        private Collider2D[] _colliders;

        private void Awake()
        {
            _piece = GetComponent<Piece>();
        }

        /*
        private void Start()
        {
            _colliders = new Collider2D[5];
        }

        private void FixedUpdate()
        {
            if(_piece.IsPlaced) return;
            
            for (int i = 0; i < _collisionPoints.Length; i++)
            {
                if (Physics2D.OverlapCircleNonAlloc(_collisionPoints[i].position, .1f, _colliders, (1 << 6)) > 1)
                {
                    _piece.IsPlaced = true;
                    _piece._rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    return;
                    
                    
                    for (int j = 0; j < _colliders.Length; j++)
                    {
                        if(_colliders[i] == null || _colliders[j].gameObject == gameObject) continue;
                        
                        _piece.IsPlaced = true;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < _collisionPoints.Length; i++)
            {
                Gizmos.DrawSphere(_collisionPoints[i].position, .1f);
            }
        }
        */

        
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