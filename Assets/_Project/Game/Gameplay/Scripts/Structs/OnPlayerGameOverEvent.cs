namespace MiniclipTrick.Game.Events
{
    public struct OnPlayerGameOverEvent
    {
        public string playerId;
        public bool isVictory;

        public OnPlayerGameOverEvent(string playerId, bool isVictory)
        {
            this.playerId = playerId;
            this.isVictory = isVictory;
        }
    }
}