using UnityEngine;

namespace MiniclipTrick.Game
{
    [CreateAssetMenu(menuName = "Settings/Pieces Settings")]
    public class PiecesSettings : ScriptableObject
    {
        [SerializeField]
        private float _gravityMultiplier = 1;
        [SerializeField]
        private float _horizontalStep = 0.5f;
        [SerializeField]
        private float _normalDescendSpeed = 4f;
        [SerializeField]
        private float _boostDescendSpeed = 12f;

        public float GravityMultiplier => _gravityMultiplier;

        public float HorizontalStep => _horizontalStep;

        public float NormalDescendSpeed => _normalDescendSpeed;

        public float BoostDescendSpeed => _boostDescendSpeed;
    }
}