using UnityEngine;

namespace MiniclipTrick.Utility
{
    [RequireComponent(typeof(RectTransform))]
    public class UI_SafeArea : MonoBehaviour
    {
        private void Awake() => Setup();

        /// <summary>
        /// Method that is called to define variables or properties of this gameObject
        /// </summary>
        private void Setup()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable() => Initialize();

        /// <summary>
        /// Method that is called to initialize any behaviour of this gameObject
        /// </summary>
        private void Initialize() => Refresh();

        private void Update()
        {
            if (_update)
                Refresh();
        }

        private void Refresh()
        {
            var safeArea = GetSafeArea();

            if (safeArea == _lastSafeArea && _lastScreenSize.x == Screen.width && _lastScreenSize.y == Screen.height
                && _lastScreenOrientation == Screen.orientation)
                return;

            _lastScreenSize.x = Screen.width;
            _lastScreenSize.y = Screen.height;
            _lastScreenOrientation = Screen.orientation;

            ApplySafeArea(safeArea);
        }

        private void ApplySafeArea(Rect rect)
        {
            _lastSafeArea = rect;

            if (_ignoreXAxis)
            {
                rect.x = 0f;
                rect.width = Screen.width;
            }

            if (_ignoreYAxis)
            {
                rect.y = 0f;
                rect.height = Screen.height;
            }

            var anchorMin = rect.position;
            var anchorMax = rect.position + rect.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }

        private static Rect GetSafeArea() => Screen.safeArea;
        
        private Rect _lastSafeArea = Rect.zero;
        private Vector2Int _lastScreenSize = Vector2Int.zero;
        private ScreenOrientation _lastScreenOrientation = ScreenOrientation.AutoRotation;

        private RectTransform _rectTransform;

        [SerializeField]
        private bool _update = true;
        [SerializeField]
        private bool _ignoreXAxis;
        [SerializeField]
        private bool _ignoreYAxis;
    }
}
