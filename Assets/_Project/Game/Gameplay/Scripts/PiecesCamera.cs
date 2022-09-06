using DG.Tweening;
using UnityEngine;

namespace MiniclipTrick.Game
{
    public class PiecesCamera : MonoBehaviour, IShakeable
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void Shake()
        {
            _camera.DOShakePosition(.2f, .2f, 50);
        }
    }
}