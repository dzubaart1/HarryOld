using HarryPoter.Core.Quests;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core.LocalManagers.Interfaces
{
    public abstract class BaseLocalManager : MonoBehaviour
    {
        [CanBeNull]
        public TargetItemsList TargetItemsList
        {
            get
            {
                return FindObjectOfType<TargetItemsList>();
            }
        }
        
        private void Awake()
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }

            gameManager.CurrentLocalManager = this;
        }
        
        public abstract void SaveSceneState();
        public abstract void AddQuestHolder(QuestHolder questHolder);
        public abstract void OnQuestHolderCompleted(QuestHolder questHolder);
    }
}