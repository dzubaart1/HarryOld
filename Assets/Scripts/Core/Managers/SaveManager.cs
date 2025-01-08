using System;
using System.Collections.Generic;
using HarryPoter.Core.Quests;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class SaveManager : MonoBehaviour
    {
        [CanBeNull]
        public static SaveManager Instance
        {
            get;
            private set;
        }
        
        private Dictionary<int, bool> _questHoldersStatus = new Dictionary<int, bool>();
        
        private int _questHolderNextID;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;   
            }
        }

        public void SaveQuestHolderState(QuestHolder questHolder)
        {
            if (!_questHoldersStatus.TryGetValue(questHolder.QuestHolderID, out _))
            {
                _questHoldersStatus.Add(questHolder.QuestHolderID, questHolder.IsComplete);
            }
            else
            {
                _questHoldersStatus[questHolder.QuestHolderID] = questHolder.IsComplete;
            }
        }

        public bool TryGetSavedQuestHolderStatus(QuestHolder questHolder, out bool status)
        {
            status = false;
            
            if (!_questHoldersStatus.TryGetValue(questHolder.QuestHolderID, out bool hasComplete))
            {
                return false;
            }

            status = hasComplete;
            return true;
        }
    }
}