using UnityEngine;

namespace MiniclipTrick.Game
{
    [CreateAssetMenu(menuName = "Settings/Level Settings")]
    public class LevelSettings : ScriptableObject
    {
        [Range(0, 50)]
        public int endLineHeight;
        [Min(0)]
        public int piecesLostToGameOver;
        public bool againstCPU;
    }
}