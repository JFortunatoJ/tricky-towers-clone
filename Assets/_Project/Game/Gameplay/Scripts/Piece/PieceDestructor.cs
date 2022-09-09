using MiniclipTrick.Game.Piece;
using UnityEngine;

namespace MiniclipTrick.Piece
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