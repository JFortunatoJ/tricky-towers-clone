using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    public class PieceMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Vector2 centerOfMass;

        public Rigidbody2D Rigidbody => _rigidbody;
        
        private PieceController _thisPiece;
        private Transform _transform;
        private float _currentSpeed;

        //Cacheando vetor de rotação porque Vector3.foward instancia um novo vetor;
        private static readonly float HORIZONTAL_SENSITIVITY = 0.5f;
        private static readonly float VERTICAL_SENSITIVITY = .7f;
        private static readonly float NORMAL_SPEED = 4f;
        private static readonly float BOOST_SPEED = 12f;
        //TODO: editar esse valores pelo editor

        private static readonly Vector3 ForwardVector = Vector3.forward;

        private Vector3 _currentPosition;

        public void Initialize(PieceController controller)
        {
            _thisPiece = controller;
            
            Rigidbody.WakeUp();
            Rigidbody.centerOfMass = centerOfMass;
            
            _transform = transform;
            _currentSpeed = NORMAL_SPEED;
            _currentPosition = _transform.position;
        }

        public void EnablePhysics()
        {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void HorizontalStep(int direction)
        {
            float targetPosition = _currentPosition.x + HORIZONTAL_SENSITIVITY * direction;
            _currentPosition.x = Mathf.Clamp(targetPosition, 0, Screen.width / Screen.dpi);
            _transform.position = _currentPosition;
        }

        public void MoveDownwards()
        {
            _currentPosition.y += -VERTICAL_SENSITIVITY * (Time.fixedDeltaTime * _currentSpeed);
            _transform.position = _currentPosition;
        }

        public void Rotate()
        {
            _transform.localEulerAngles += ForwardVector * 90f;
        }

        public void BoostSpeed()
        {
            _currentSpeed = Mathf.Clamp(_currentSpeed + (Time.deltaTime * 20f), NORMAL_SPEED, BOOST_SPEED);
        }

        public void ResetSpeed()
        {
            _currentSpeed = NORMAL_SPEED;
        }
    }
}