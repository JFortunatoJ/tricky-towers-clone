using System;
using MiniclipTrick.Game.Piece;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class PieceDetectorRay
{
    public Vector2 raySource;
    [Space]
    [SerializeField]
    public float rayDistance = 1;
    [SerializeField]
    [ReadOnly]
    private bool rayHasPiece;
    
    private RaycastHit2D[] _rayResults;
    private PieceController _detectedPiece;
    
    private static readonly Vector2 RightVector = Vector2.right;
    private static readonly int PieceLayer = 1 << 6;
    
    public Action<bool> onStatusChanged;

    public PieceController DetectedPiece
    {
        get => _detectedPiece;
        private set => _detectedPiece = value;
    }

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

    public void Init()
    {
        _rayResults = new RaycastHit2D[1];
    }

    public bool CheckPiece()
    {
        RayHasPiece = IsCollidingWithPiece();
        return RayHasPiece;
    }

    private bool IsCollidingWithPiece()
    {
        if (Physics2D.RaycastNonAlloc(raySource, RightVector, _rayResults, rayDistance, PieceLayer) == 0) return false;
        
        for (int i = 0; i < _rayResults.Length; i++)
        {
            if(_rayResults[i].collider == null) continue;

            if (_rayResults[i].transform.TryGetComponent(out _detectedPiece) && _detectedPiece.IsPlaced)
            {
                return true;
            }
        }

        return false;
    }
}
