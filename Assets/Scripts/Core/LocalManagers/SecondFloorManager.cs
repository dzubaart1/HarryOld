using System.Collections.Generic;
using System.Linq;
using HarryPoter.Core.LocalManagers.Interfaces;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace HarryPoter.Core.LocalManagers
{
    public class SecondFloorManager : BaseLocalManager
    {
        [SerializeField] private List<QuestHolder> _questHolders = new List<QuestHolder>();
        [SerializeField] private string _sceneNameToTeleportAtTheEnd;
        
        public void Start()
        {
            InitQuestHolders();
        }
        
        public override void OnQuestHolderCompleted(QuestHolder questHolder)
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }

            Player player = gameManager.GetPlayer();

            if (player == null)
            {
                return;
            }
            
            if (_questHolders.All(questHolder => questHolder.IsComplete))
            {
                gameManager.LoadScene(_sceneNameToTeleportAtTheEnd);
            }
        }

        private void InitQuestHolders()
        {
            foreach (var questHolder in _questHolders)
            {
                questHolder.Init(this);
            }
        }
    }
}