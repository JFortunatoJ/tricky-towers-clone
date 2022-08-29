using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Piece : MonoBehaviour
    {
        [SerializeField] private PieceRotation[] _rotations;

        [SerializeField] private Vector2 centerOfMass;

        public Rigidbody2D _rigidbody;
        private Transform _transform;

        [SerializeField, ReadOnly] private bool _isPlaced;
        private bool _canRotate;
        private int _rotationIndex;
        private float _currentSpeed;
        private Vector3 _previousPosition;

        //Cacheando vetor de rotação porque Vector3.foward instancia um novo vetor;
        private static readonly Vector3 ForwardVector = Vector3.forward;
        private static readonly Vector3 DownVector = Vector3.down;
        private static Vector3 MovementVector = Vector3.right * .5f;

        public static readonly float NORMAL_SPEED = 4f;
        public static readonly float BOOST_SPEED = 12f;

        public Action OnCollideWithPiece;

        public int RotationIndex
        {
            get => _rotationIndex;
            private set
            {
                if (value >= _rotations.Length)
                {
                    _rotationIndex = 0;
                }
                else if (value < 0)
                {
                    _rotationIndex = _rotations.Length - 1;
                }
                else
                {
                    _rotationIndex = value;
                }
            }
        }

        public bool IsPlaced
        {
            get => _isPlaced; 
            set
            {
                _isPlaced = value;
                _rigidbody.velocity = Vector3.zero;
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;

            _canRotate = true;
            _currentSpeed = NORMAL_SPEED;
        }

        private void Start()
        {
            _rigidbody.WakeUp();
            _rigidbody.centerOfMass = centerOfMass;
        }

        public void MoveHorizontally(int direction)
        {
            _transform.position += MovementVector * direction;
        }

        public void MoveDownwards()
        {
            _previousPosition = _rigidbody.position;
            _transform.position += DownVector * (Time.fixedDeltaTime * _currentSpeed);
        }

        public void ReturnToPreviousPosition()
        {
            _transform.position = _previousPosition;
        }

        public void Rotate()
        {
            RotationIndex++;
            _transform.localEulerAngles = ForwardVector * (_rotations[_rotationIndex].Rotation);
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