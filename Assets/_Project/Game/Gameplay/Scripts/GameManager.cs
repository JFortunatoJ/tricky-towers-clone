using System.Collections.Generic;
using Blazewing.DataEvent;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Player;
using MiniclipTrick.GameOver;
using UnityEngine;

namespace MiniclipTrick.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _player_1;

        private List<PlayerController> _playersList;

        private void Start()
        {
            _playersList = new List<PlayerController>();
            _playersList.Add(_player_1);
            
            _player_1.Init(5);
        }

        private void OnEnable()
        {
            DataEvent.Register<OnPlayerGameOverEvent>(OnPlayerGameOver);
        }

        private void OnDisable()
        {
            DataEvent.Unregister<OnPlayerGameOverEvent>(OnPlayerGameOver);
        }

        private void OnPlayerGameOver(OnPlayerGameOverEvent eventData)
        {
            GameOverController.Show(eventData.isVictory ? VictoryScreenController.SCENE_NAME : DefeatScreenController.SCENE_NAME);
            print($"Jogador: {eventData.playerId} deu game over. Vitória: {eventData.isVictory}");

            for (int i = 0; i < _playersList.Count; i++)
            {
                _playersList[i].GameOver();
            }
        }

        private PlayerController GetPlayerById(string id)
        {
            return _playersList.Find(controller => controller.playerId == id);
        }
    }
}