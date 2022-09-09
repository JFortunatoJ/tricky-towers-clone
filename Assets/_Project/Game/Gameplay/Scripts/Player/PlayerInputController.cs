using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MiniclipTrick.Game.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        public void Init()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            _rayResults = new List<RaycastResult>();
            
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
            if(PointerOverUI()) return;
            
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
        
        private bool PointerOverUI()
        {
            _eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _rayResults);

            List<RectTransform> uiResults = new List<RectTransform>();
            foreach (var rayResult in _rayResults)
            {
                if (rayResult.gameObject.TryGetComponent(out RectTransform rect))
                {
                    uiResults.Add(rect);
                }
            }

            return uiResults.Count > 0;
        }

        [SerializeField]
        private float _dragDeadZone = 10f;
        
        private PlayerInputActions _playerInput;
        private float _previousPos;

        public Action onRotateInput;
        public Action<float> onMoveHorizontallyInput;
        public Action<bool> onSpeedUpPiece;
        
        private PointerEventData _eventDataCurrentPosition;
        private List<RaycastResult> _rayResults;

    }
}