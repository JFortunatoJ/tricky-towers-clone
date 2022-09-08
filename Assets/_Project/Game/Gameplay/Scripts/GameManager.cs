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
        protected Transform _playersHolder;

        protected List<PlayerController> _playersList;

        protected virtual void OnEnable()
        {
            DataEvent.Register<OnPlayerGameOverEvent>(OnPlayerGameOver);
        }

        protected virtual void OnDisable()
        {
            DataEvent.Unregister<OnPlayerGameOverEvent>(OnPlayerGameOver);
        }

        private void Start()
        {
            InstantiatePlayers();
        }

        protected virtual void StartGame()
        {
            DataEvent.Notify(new OnGameStartedEvent());
        }

        protected virtual void InstantiatePlayers()
        {
        }

        protected virtual void OnPlayerInstantiated(PlayerController player)
        {
            _playersList ??= new List<PlayerController>();
            _playersList.Add(player);
        }

        protected virtual void OnPlayerGameOver(OnPlayerGameOverEvent eventData)
        {
            GameOverController.Show(eventData.isVictory
                ? VictoryScreenController.SCENE_NAME
                : DefeatScreenController.SCENE_NAME);
            print($"Jogador: {eventData.playerId} deu game over. Vitória: {eventData.isVictory}");

            for (int i = 0; i < _playersList.Count; i++)
            {
                _playersList[i].GameOver();
            }
        }

        protected PlayerController GetPlayerById(string id)
        {
            return _playersList.Find(controller => controller.playerId == id);
        }
    }
}