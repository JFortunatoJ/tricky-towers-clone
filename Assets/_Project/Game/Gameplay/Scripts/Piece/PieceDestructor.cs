using MiniclipTest.Game.Piece;
using UnityEngine;

namespace MiniclipTest.Piece
{
    public class PieceDestructor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.attachedRigidbody.TryGetComponent(out PieceController piece)) return;
            
            piece.DestroyPiece();
        }
    }
}