using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Game : MonoBehaviour
    {
        public float StartGameTime { get; private set; }
        public float FinishGameTime { get; private set; }
        public bool GameFinished { get; private set; }

        public IReadOnlyList<EListItem> CompletedListItems => _completedListItems;

        private List<EListItem> _completedListItems = new List<EListItem>();
        
        public void StartGame()
        {
            StartGameTime = Time.time;
            GameFinished = false;
        }

        public void FinishGame()
        {
            FinishGameTime = Time.time;
            GameFinished = true;
        }

        public void OnTargetItemPickedUp(EListItem listItem)
        {
            _completedListItems.Add(listItem);
        }
    }
}