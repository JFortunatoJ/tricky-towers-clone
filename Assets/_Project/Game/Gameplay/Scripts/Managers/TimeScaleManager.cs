using Blazewing;
using MiniclipTest.Game.Events;
using MiniclipTest.Utility;
using UnityEngine;

namespace MiniclipTest.Game
{
    public class TimeScaleManager : PersistentSingleton<TimeScaleManager>
    {
        private void OnEnable()
        {
            DataEvent.Register<OnPlayerGameOverEvent>(OnGameOver);
            DataEvent.Register<OnPauseEvent>(OnPauseStatusChanged);
        }
        
        private void OnDisable()
        {
            DataEvent.Unregister<OnPlayerGameOverEvent>(OnGameOver);
            DataEvent.Unregister<OnPauseEvent>(OnPauseStatusChanged);
        }

        private void OnPauseStatusChanged(OnPauseEvent eventData)
        {
            Time.timeScale = eventData.isPaused ? 0 : 1;
        }

        private void OnGameOver(OnPlayerGameOverEvent eventData)
        {
            Time.timeScale = 0;
        }
    }
}
