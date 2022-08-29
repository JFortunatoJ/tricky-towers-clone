using UnityEngine;

namespace MiniclipTrick.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player_1;

        private void Start()
        {
            _player_1.Init();
        }
    }
}