using System;

namespace HarryPoter.Core
{
    public class GameCycleService : IService
    {
        public event Action GameStartedEvent;
        public event Action GameFinishedEvent;
        public event Action AllItemsFoundEvent;
        
        public void Initialize()
        {
            StartGame();
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }

        public void FinishGame()
        {
            GameFinishedEvent?.Invoke();
        }

        private void StartGame()
        {
            GameStartedEvent?.Invoke();
        }

        public void AllItemsFound()
        {
            AllItemsFoundEvent?.Invoke();
        }
    }
}