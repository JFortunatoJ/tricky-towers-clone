using UnityEngine;

public class TowerHeightChecker : MonoBehaviour
{
    [SerializeField]
    private float _minRayHeight;
    [SerializeField]
    private float _maxRayHeight;
    [Space]
    [SerializeField] private float _rayRaiseAmount = 5;
    
    public PieceDetectorRay _rayA;
    public PieceDetectorRay _rayB;

    private void Start()
    {
        _rayA.Init();
        _rayB.Init();
        
        _rayA.onStatusChanged += OnRayAStatusChanged;
        _rayB.onStatusChanged += OnRayBStatusChanged;
    }

    private void FixedUpdate()
    {
        _rayA.CheckPiece();
        _rayB.CheckPiece();
    }
    
    private void OnRayAStatusChanged(bool status)
    {
        print($"Ray A: {status}");
         if (status)
        {
            _rayB.raySource.y = _rayA.raySource.y;
            _rayA.raySource.y = Mathf.Clamp(_rayA.raySource.y + _rayRaiseAmount, _minRayHeight, _maxRayHeight);
        }
    }
    
    private void OnRayBStatusChanged(bool status)
    {
        print($"Ray B: {status}");
        if (!status)
        {
            _rayA.raySource.y = _rayB.raySource.y;
            _rayB.raySource.y = Mathf.Clamp(_rayB.raySource.y - _rayRaiseAmount, _minRayHeight, _maxRayHeight);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_rayA.raySource, Vector3.right * _rayA.rayDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_rayB.raySource, Vector3.right * _rayB.rayDistance);
    }
#endif
}
