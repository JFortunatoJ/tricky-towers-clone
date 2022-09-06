using System;
using UnityEngine;

namespace MiniclipTrick.Game.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Action onRotateInput;
        public Action<float> onMovePieceHorizontally;
        public Action<bool> onSpeedUpPiece;

        private void Update()
        {

        }
    }
}