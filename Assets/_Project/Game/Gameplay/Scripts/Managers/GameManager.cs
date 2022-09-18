using System.Collections.Generic;
using Blazewing;
using MiniclipTest.Game.Events;
using MiniclipTest.Game.Player;
using MiniclipTest.GameOver;
using MiniclipTest.Utility;
using UnityEngine;

namespace MiniclipTest.Game
{
    public class GameManager : StaticInstance<GameManager>
    {
        protected virtual void OnEnable()
        {
            DataEvent.Register<OnPlayerGameOverEvent>(OnPlayerGameOver);
        }

        protected virtual void OnDisable()
        {
            DataEvent.Unregister<OnPlayerGameOverEvent>(OnPlayerGameOver);
        }

        protected virtual void Start()
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
            BaseController.Show(eventData.isVictory
                ? VictoryScreenController.SCENE_NAME
                : DefeatScreenController.SCENE_NAME);

            for (int i = 0; i < _playersList.Count; i++)
            {
                _playersList[i].GameOver();
            }
        }

        protected PlayerController GetPlayerById(string id)
        {
            return _playersList.Find(controller => controller.playerId == id);
        }
        
        [SerializeField]
        protected Transform _playersHolder;

        protected List<PlayerController> _playersList;
        
        public bool IsAgainstCPU { get; protected set; }
    }
}