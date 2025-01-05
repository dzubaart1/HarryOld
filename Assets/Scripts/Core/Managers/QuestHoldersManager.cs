using System.Collections.Generic;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace HarryPoter.Core
{
    public class QuestHoldersManager : MonoBehaviour
    {
        private Dictionary<int, bool> _questHoldersStatus = new Dictionary<int, bool>();
        
        private int _questHolderNextID;

        public void InitQuestHolder()
        {
            QuestHolder[] questHolders = FindObjectsOfType<QuestHolder>();

            foreach (var questHolder in questHolders)
            {
                if (!_questHoldersStatus.TryGetValue(questHolder.QuestHolderID, out bool hasComplete))
                {
                    _questHoldersStatus.Add(questHolder.QuestHolderID, false);
                }
                else
                {
                    questHolder.Init(this, hasComplete);
                }
            }
        }

        public void OnQuestHolderComplete(QuestHolder questHolder)
        {
            
        }
    }
}