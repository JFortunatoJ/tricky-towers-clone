using Blazewing;
using MiniclipTest.Game.Events;
using UnityEngine;

public class TowerHeightChecker : MonoBehaviour
{
    public void Init(string towerOwnerId)
    {
        _towerOwnerId = towerOwnerId;

        _rayA.onStatusChanged += OnRayAStatusChanged;
        _rayB.onStatusChanged += OnRayBStatusChanged;
        
        _focusPositionY = _rayA.raySource.y;
    }

    private void FixedUpdate()
    {
        _rayA.CheckPiece();
        _rayB.CheckPiece();
    }
    
    private void OnRayAStatusChanged(bool status)
    {
        if (!status) return;

        do
        {
            _focusPositionY = _rayB.raySource.y = _rayA.raySource.y;
            _rayA.raySource.y = Mathf.Clamp(_rayA.raySource.y + _rayRaiseAmount, _minRayHeight,
                MiniclipTest.Game.GameSettings.Instance.FinishLineHeight + _rayRaiseAmount * 2f);

        } while (_rayA.CheckPiece());
        
        DataEvent.Notify(new OnTowerHeightChanged(_towerOwnerId, _focusPositionY));
    }
    
    private void OnRayBStatusChanged(bool status)
    {
        if (status) return;
        
        _rayA.raySource.y = _rayB.raySource.y;
        _focusPositionY = _rayB.raySource.y = Mathf.Clamp(_rayB.raySource.y - _rayRaiseAmount, _minRayHeight, MiniclipTest.Game.GameSettings.Instance.FinishLineHeight);
        
        DataEvent.Notify(new OnTowerHeightChanged(_towerOwnerId, _focusPositionY));
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
    
    [SerializeField]
    private float _minRayHeight;
    [Space]
    [SerializeField] private float _rayRaiseAmount = 5;

    private float _focusPositionY;
    private string _towerOwnerId;
    
    public PieceDetectorRay _rayA;
    public PieceDetectorRay _rayB;
}
