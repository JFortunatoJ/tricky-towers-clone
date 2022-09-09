using UnityEngine;
using Zenject;

namespace MiniclipTrick.Game.Piece
{
    public class PieceMovement : MonoBehaviour
    {
        [Inject]
        public void Construct(PiecesSettings settings)
        {
            _settings = settings;
        }

        public void Initialize(PieceController controller)
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            
            _thisPiece = controller;
            
            Rigidbody.WakeUp();
            Rigidbody.centerOfMass = centerOfMass;
            Rigidbody.gravityScale = _settings.GravityMultiplier;
            
            _transform = transform;
            _centerPosition = _transform.position.x;
            _currentSpeed = _settings.NormalDescendSpeed;
            _currentPosition = _transform.localPosition;

            _screenHalfWorldSize = Screen.currentResolution.height / Screen.dpi;
            /*
            print(_screenHalfWorldSize);
            print(Screen.currentResolution.height);
            print(Screen.currentResolution.width);
            */
        }

        public void EnablePhysics()
        {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void HorizontalStep(int direction)
        {
            float targetPosition = _currentPosition.x + _settings.HorizontalStep * direction;
            _currentPosition.x = Mathf.Clamp(targetPosition, -_screenHalfWorldSize, _screenHalfWorldSize);
            _transform.localPosition = _currentPosition;
        }

        public void MoveDownwards()
        {
            _currentPosition.y -= Time.deltaTime * _currentSpeed;
            _transform.localPosition = _currentPosition;
        }

        public void Rotate()
        {
            _transform.localEulerAngles += ForwardVector * 90f;
        }

        public void BoostSpeed()
        {
            _currentSpeed = Mathf.Clamp(_currentSpeed + (Time.deltaTime * 20f), _settings.NormalDescendSpeed, _settings.BoostDescendSpeed);
        }

        public void ResetSpeed()
        {
            _currentSpeed = _settings.NormalDescendSpeed;
        }

        [SerializeField]
        private Vector2 centerOfMass;

        public Rigidbody2D Rigidbody { get; private set; }

        public Vector3 LocalEulerAngles => _transform.localEulerAngles;

        public Vector3 Position => _transform.position;
        
        public Vector3 LocalPosition => _transform.localPosition;

        private PiecesSettings _settings;
        private PieceController _thisPiece;
        private Transform _transform;
        private float _currentSpeed;
        private float _screenHalfWorldSize;

        private static readonly Vector3 ForwardVector = Vector3.forward;

        private Vector3 _currentPosition;
        private float _centerPosition;
    }
}