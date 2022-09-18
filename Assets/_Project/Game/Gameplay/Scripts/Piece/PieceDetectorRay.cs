using System;
using MiniclipTest.Game.Piece;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class PieceDetectorRay
{
    public bool CheckPiece()
    {
        RayHasPiece = IsCollidingWithPiece();
        return RayHasPiece;
    }

    private bool IsCollidingWithPiece()
    {
        RaycastHit2D[] rayResults = new RaycastHit2D[2];
        if (Physics2D.RaycastNonAlloc(raySource, RightVector, rayResults, rayDistance, PieceLayer) == 0) return false;
        
        for (int i = 0; i < rayResults.Length; i++)
        {
            if(rayResults[i].collider is null) continue;

            if (rayResults[i].transform.TryGetComponent(out PieceController piece) && piece.IsPlaced)
            {
                return true;
            }
        }

        return false;
    }

    public Vector2 raySource;
    [Space]
    [SerializeField]
    public float rayDistance = 1;
    [SerializeField]
    [ReadOnly]
    private bool rayHasPiece;

    private static readonly Vector2 RightVector = Vector2.right;
    private static readonly int PieceLayer = 1 << 6;
    
    public Action<bool> onStatusChanged;

    public bool RayHasPiece
    {
        get => rayHasPiece;
        private set
        {
            if(rayHasPiece == value) return;

            rayHasPiece = value;
            onStatusChanged?.Invoke(rayHasPiece);
        }
    }
}
