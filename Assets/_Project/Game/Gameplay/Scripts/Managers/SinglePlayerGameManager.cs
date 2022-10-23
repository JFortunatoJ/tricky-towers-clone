using MiniclipTest.Game.Player;

namespace MiniclipTest.Game
{
    public class SinglePlayerGameManager : GameManager
    {
        protected override void SetupInstance()
        {
            IsAgainstCPU = false;
            base.SetupInstance();
        }

        protected override void InstantiatePlayers()
        {
            PlayerFactory<HumanController>.CreateNewPlayer("Player", _playersHolder, 0, OnPlayerInstantiated);
        }

        protected override void OnPlayerInstantiated(PlayerController player)
        {
            base.OnPlayerInstantiated(player);

            StartGame();
        }
    }
}