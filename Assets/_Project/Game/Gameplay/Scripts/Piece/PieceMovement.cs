using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace MiniclipTest.Game.Piece
{
    public class PieceMovement : MonoBehaviour
    {
        public void Initialize(PieceController controller)
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            
            _thisPiece = controller;
            
            Rigidbody.WakeUp();
            Rigidbody.centerOfMass = _centerOfMass;
            Rigidbody.gravityScale = GameSettings.Instance.GravityMultiplier;
            
            _transform = transform;
            _currentSpeed = GameSettings.Instance.PieceNormalDescendSpeed;
            _currentPosition = _transform.localPosition;

            _screenHalfWorldSize = Screen.currentResolution.height / Screen.dpi;
        }

        public void EnablePhysics()
        {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void HorizontalStep(int direction)
        {
            if(!CanMove) return;
            
            float targetPosition = _currentPosition.x + GameSettings.Instance.PieceHorizontalStep * direction;
            _currentPosition.x = Mathf.Clamp(targetPosition, -_screenHalfWorldSize, _screenHalfWorldSize);
            _transform.localPosition = _currentPosition;
        }

        public void MoveDownwards()
        {
            if(!CanMove) return;
            
            _currentPosition.y -= Time.deltaTime * _currentSpeed;
            _transform.localPosition = _currentPosition;
        }

        public void Rotate()
        {
            if(!CanMove) return;
            
            _transform.localEulerAngles += ForwardVector * 90f;
        }

        public void BoostSpeed()
        {
            DOTween.To(() => _currentSpeed, x => _currentSpeed = x, GameSettings.Instance.PieceBoostDescendSpeed, .25f);
        }

        public void ResetSpeed()
        {
            _currentSpeed = GameSettings.Instance.PieceNormalDescendSpeed;
        }

        [FormerlySerializedAs("centerOfMass")]
        [SerializeField]
        private Vector2 _centerOfMass;

        public Rigidbody2D Rigidbody { get; private set; }

        public Vector3 LocalEulerAngles => _transform.localEulerAngles;

        public Vector3 Position => _transform.position;
        
        public Vector3 LocalPosition => _transform.localPosition;
        
        public bool CanMove { private get; set; }
        
        private PieceController _thisPiece;
        private Transform _transform;
        private float _currentSpeed;
        private float _screenHalfWorldSize;
        private Vector3 _currentPosition;

        private static readonly Vector3 ForwardVector = Vector3.forward;
    }
}