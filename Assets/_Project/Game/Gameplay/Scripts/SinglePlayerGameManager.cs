using MiniclipTrick.Game.Player;

namespace MiniclipTrick.Game
{
    public class SinglePlayerGameManager : GameManager
    {
        protected override void InstantiatePlayers()
        {
            PlayerFactory.CreateNewHumanPlayer("Player_1", _playersHolder, OnPlayerInstantiated);
        }

        protected override void OnPlayerInstantiated(PlayerController player)
        {
            base.OnPlayerInstantiated(player);

            StartGame();
        }
    }
}