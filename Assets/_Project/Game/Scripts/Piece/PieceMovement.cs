using UnityEngine;

namespace MiniclipTrick.Game.Piece
{
    public class PieceMovement : MonoBehaviour
    {
        private Transform _transform;
        private float _currentSpeed;

        //Cacheando vetor de rotação porque Vector3.foward instancia um novo vetor;
        private static readonly float HORIZONTAL_SENSITIVITY = 0.5f;
        private static readonly float VERTICAL_SENSITIVITY = 1f;
        private static readonly float NORMAL_SPEED = 4f;
        private static readonly float BOOST_SPEED = 12f;
        //TODO: editar esse valores pelo editor

        private static readonly Vector3 ForwardVector = Vector3.forward;

        private Vector3 _currentPosition;

        public void Initialize()
        {
            _transform = transform;
            _currentSpeed = NORMAL_SPEED;
            _currentPosition = _transform.position;
        }

        public void MoveHorizontally(int direction)
        {
            _currentPosition.x += HORIZONTAL_SENSITIVITY * direction;
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