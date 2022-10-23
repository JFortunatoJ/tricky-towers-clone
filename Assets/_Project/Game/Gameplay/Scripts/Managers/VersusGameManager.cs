using MiniclipTest.Game.Events;
using MiniclipTest.Game.Player;
using MiniclipTest.GameOver;

namespace MiniclipTest.Game
{
    public class VersusGameManager : GameManager
    {
        protected override void SetupInstance()
        {
            IsAgainstCPU = true;
            base.SetupInstance();
        }

        protected override void InstantiatePlayers()
        {
            PlayerFactory<HumanController>.CreateNewPlayer("Player", _playersHolder, 0, controller =>
            {
                _humanPlayer = controller;
                OnPlayerInstantiated(controller);
            });
            
            PlayerFactory<AiController>.CreateNewAIPlayer("CPU", _playersHolder, 60, controller =>
            {
                _cpuPlayer = controller;
                OnPlayerInstantiated(controller);
            });
        }

        protected override void OnPlayerInstantiated(PlayerController player)
        {
            base.OnPlayerInstantiated(player);

            _playersCount++;
            if (_playersCount == 2)
            {
                StartGame();
            }
        }

        protected override void OnPlayerGameOver(OnPlayerGameOverEvent eventData)
        {
            print($"Jogador: {eventData.playerId} deu game over. Vitória: {eventData.isVictory}");
            
            if (eventData.isVictory)
            {
                GameOverController.Show(eventData.playerId.Equals(_humanPlayer.playerId)
                    ? VictoryScreenController.SCENE_NAME
                    : DefeatScreenController.SCENE_NAME);
            }
            else
            {
                GameOverController.Show(eventData.playerId.Equals(_cpuPlayer.playerId)
                    ? VictoryScreenController.SCENE_NAME
                    : DefeatScreenController.SCENE_NAME);
            }

            for (int i = 0; i < _playersList.Count; i++)
            {
                _playersList[i].GameOver();
            }
        }

        private int _playersCount = 0;
        private HumanController _humanPlayer;
        private AiController _cpuPlayer;
    }
}