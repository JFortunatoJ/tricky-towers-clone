using MiniclipTest.Game.Piece;
using UnityEngine;

namespace MiniclipTest.Game.Scriptables
{
    [CreateAssetMenu(menuName = "Scriptables/Game/Pieces Container")]
    public class PiecesContainerScriptable : ScriptableObject
    {
        [SerializeField] private PieceController[] _piecesPrefabs;

        public int Count => _piecesPrefabs.Length;

        public PieceController GetPieceByIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
#if UNITY_EDITOR
                Debug.LogError($"Inválid index: {index}");
#endif
                return GetRandomPiece();
            }

            return _piecesPrefabs[index];
        }

        public PieceController GetRandomPiece()
        {
            return _piecesPrefabs[Random.Range(0, Count)];
        }
    }
}