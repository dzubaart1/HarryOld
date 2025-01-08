using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Game : MonoBehaviour
    {
        public float StartGameTime { get; private set; }
        public float FinishGameTime { get; private set; }

        public bool HasCompleteFirstFloor { get; set; } = false;
        public bool HasCompleteSecondFloor { get; set; } = false;

        public IReadOnlyList<EListItem> CompletedListItems => _completedListItems;

        private List<EListItem> _completedListItems = new List<EListItem>();
        
        public void StartGame()
        {
            StartGameTime = Time.time;
        }

        public void FinishGame()
        {
            FinishGameTime = Time.time;
        }

        public void OnTargetItemPickedUp(EListItem listItem)
        {
            _completedListItems.Add(listItem);
        }
    }
}