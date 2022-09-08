using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniclipTrick.Game.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        public void Init()
        {
            _playerInput = new PlayerInputActions();

            _playerInput.PiecesActions.Rotate.performed += OnRotatePiece;
            _playerInput.PiecesActions.TouchDown.performed += OnTouchDown;
            _playerInput.PiecesActions.Move.performed += OnMove;
            
            _playerInput.Enable();
        }
        
        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void OnRotatePiece(InputAction.CallbackContext obj)
        {
            onRotateInput?.Invoke();
        }
        
        private void OnTouchDown(InputAction.CallbackContext obj)
        {
            _previousPos = Touchscreen.current.position.ReadValue().x;
        }
        
        private void OnMove(InputAction.CallbackContext obj)
        {
            Vector2 value = obj.ReadValue<Vector2>();
            
            float diff = value.x - _previousPos;
            int direction = diff < 0 ? -1 : 1;
            if (Mathf.Abs(diff) > Screen.width / _dragDeadZone)
            {
                _previousPos = value.x;
                onMoveHorizontallyInput?.Invoke(direction);
            }
        }
        
        [SerializeField]
        private float _dragDeadZone = 10f;
        
        private PlayerInputActions _playerInput;
        private float _previousPos;

        public Action onRotateInput;
        public Action<float> onMoveHorizontallyInput;
        public Action<bool> onSpeedUpPiece;
    }
}