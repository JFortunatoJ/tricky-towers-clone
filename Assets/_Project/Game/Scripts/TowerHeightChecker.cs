using System;
using MiniclipTrick.Game.Piece;
using UnityEngine;

public class TowerHeightChecker : MonoBehaviour
{
    [SerializeField]
    private Vector2 _raySource_A;
    [SerializeField]
    private Vector2 _raySource_B;
    [Space]
    [SerializeField]
    private float _rayDistance = 1;
    [SerializeField]
    [Range(1, 5)]
    private int _raycastHitsAmount;
    
    private static readonly Vector2 RightVector = Vector2.right;
    private static readonly int PieceLayer = 1 << 6;

    private bool _rayAHasPiece;
    private bool _rayBHasPiece;
    private RaycastHit2D[] _rayAResults;
    private RaycastHit2D[] _rayBResults;

    private void Start()
    {
        _rayAResults = new RaycastHit2D[_raycastHitsAmount];
        _rayBResults = new RaycastHit2D[_raycastHitsAmount];
    }

    private void FixedUpdate()
    {
        _rayAHasPiece = CheckRayCast(_raySource_A, _rayAResults);
        _rayBHasPiece = CheckRayCast(_raySource_B, _rayBResults);
        
        //TODO: O de baixo sempre será true e o de cima falso
        //Quando o de cima for true, sobe os dois
    }

    private bool CheckRayCast(Vector2 origin, RaycastHit2D[] results)
    {
        if (Physics2D.RaycastNonAlloc(origin, RightVector, results, _rayDistance, PieceLayer) == 0) return false;
        
        for (int i = 0; i < _raycastHitsAmount; i++)
        {
            if(results[i].collider == null) continue;

            if (results[i].transform.TryGetComponent(out PieceController piece) && piece.IsPlaced)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_raySource_A, RightVector * _rayDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_raySource_B, RightVector * _rayDistance);
    }
}
