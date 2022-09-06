namespace MiniclipTrick.Game.Events
{
    public struct OnPauseEvent
    {
        public bool isPaused;

        public OnPauseEvent(bool isPaused)
        {
            this.isPaused = isPaused;
        }
    }
}