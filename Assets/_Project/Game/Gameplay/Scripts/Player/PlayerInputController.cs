using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MiniclipTest.Game.Player
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
            _playerInput.PiecesActions.TouchUp.performed += OnTouchUp;
            _playerInput.PiecesActions.HorizontalMove.performed += OnHorizontalMove;
            _playerInput.PiecesActions.VerticalMove.performed += OnVerticalMove;

            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void OnRotatePiece(InputAction.CallbackContext obj)
        {
            if (PointerOverUI()) return;

            onRotateInput?.Invoke();
        }

        private void OnTouchDown(InputAction.CallbackContext obj)
        {
#if !UNITY_EDITOR
            _previousFingerPos = Touchscreen.current.position.ReadValue().x;
#endif
        }
        
        private void OnTouchUp(InputAction.CallbackContext obj)
        {
            SpeedUpStatus = false;
        }

        private void OnHorizontalMove(InputAction.CallbackContext obj)
        {
            Vector2 value = obj.ReadValue<Vector2>();

#if !UNITY_EDITOR
            float diff = value.x - _previousFingerPos;
            int direction = diff < 0 ? -1 : 1;
            if (Mathf.Abs(diff) > Screen.width / _minHorizontalMovDist)
            {
                _previousFingerPos = value.x;
                onMoveHorizontallyInput?.Invoke(direction);
            }
#else
            onMoveHorizontallyInput?.Invoke(value.x);
#endif
        }

        private void OnVerticalMove(InputAction.CallbackContext obj)
        {
            Vector2 value = obj.ReadValue<Vector2>();

#if !UNITY_EDITOR
            if (value.y <= _minVerticalMovDist && MathF.Abs(value.x) < 1)
            {
                SpeedUpStatus = true;
            }
#else
            if (value.y == -1)
            {
                SpeedUpStatus = true;
            }
            else
            {
                SpeedUpStatus = false;
            }
#endif
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
        [Range(0, 10f)]
        private float _minHorizontalMovDist = 10f;
        [SerializeField]
        [Range(-10f, 0)]
        private float _minVerticalMovDist = -30f;
        
        private float _previousFingerPos;
        private PlayerInputActions _playerInput;
        private PointerEventData _eventDataCurrentPosition;
        private List<RaycastResult> _rayResults;
        private bool _speedUpStatus;

        private bool SpeedUpStatus
        {
            set
            {
                if(_speedUpStatus == value) return;

                _speedUpStatus = value;
                onSpeedUpPiece?.Invoke(_speedUpStatus);
            }
        }

        public Action onRotateInput;
        public Action<float> onMoveHorizontallyInput;
        public Action<bool> onSpeedUpPiece;
    }
}