using MiniclipTrick.Game.Player;

namespace MiniclipTrick.Game
{
    public class SinglePlayerGameManager : GameManager
    {
        protected override void InstantiatePlayers()
        {
            PlayerFactory.CreateNewHumanPlayer("Player", _playersHolder, 0, OnPlayerInstantiated);
        }

        protected override void OnPlayerInstantiated(PlayerController player)
        {
            base.OnPlayerInstantiated(player);

            StartGame();
        }
    }
}