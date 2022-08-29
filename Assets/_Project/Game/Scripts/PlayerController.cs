using MiniclipTrick.Game.Piece;
using UnityEngine;

namespace MiniclipTrick.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PiecesController _piecesSpawner;

        private void Awake()
        {
            _piecesSpawner.SetupPiecesPool();
        }

        public void Init()
        {
            _piecesSpawner.SpawnPiece();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _piecesSpawner.SpawnPiece();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _piecesSpawner.CurrentPiece.Rotate();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _piecesSpawner.CurrentPiece.MoveHorizontally(-1);
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _piecesSpawner.CurrentPiece.MoveHorizontally(1);
            }

            /*
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _piecesSpawner.CurrentPiece.BoostSpeed();
            }
            
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                _piecesSpawner.CurrentPiece.ResetSpeed();
            }
            */
        }
    }
}