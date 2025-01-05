using System;
using System.Collections.Generic;
using HarryPoter.Core.Quests;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Game : MonoBehaviour
    {
        public float StartGameTime { get; private set; }
        public float FinishGameTime { get; private set; }

        [CanBeNull]
        public ItemsList ItemsList
        {
            get
            {
                return FindObjectOfType<ItemsList>();
            }
        }
        
        private List<QuestHolder> _questHolders = new List<QuestHolder>();
        
        public void StartGame()
        {
            StartGameTime = DateTime.Now.Minute;
        }

        public void FinishGame()
        {
            FinishGameTime = DateTime.Now.Minute;
        }
        
        public void OnQuestHolderComplete(QuestHolder questHolder)
        {
            ItemsList itemsList = ItemsList;
            if (itemsList == null)
            {
                return;
            }

            itemsList.OnQuestHolderComplete(questHolder.ListItem);
        }
    }
}