using System.Collections.Generic;
using System.Linq;
using HarryPoter.Core.LocalManagers.Interfaces;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace HarryPoter.Core.LocalManagers
{
    public class FirstFloorManager : BaseLocalManager
    {
        [Header("Configs")]
        [SerializeField] private float _teleportPosForwardMultiplayer = 1f;
        
        [Space]
        [SerializeField] private List<QuestHolder> _questHolders = new List<QuestHolder>();
        
        private void Start()
        {
            InitQuestHolders();
        }
        
        public override void AddQuestHolder(QuestHolder questHolder)
        {
            if (_questHolders.Contains(questHolder))
            {
                return;
            }
            
            _questHolders.Add(questHolder);
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
            
            TeleportManager teleportManager = TeleportManager.Instance;
            if (teleportManager == null)
            {
                return;
            }
            
            questHolder.TargetItem.gameObject.SetActive(true);
            
            Vector3 teleportPos = player.Head.position + player.Head.forward * _teleportPosForwardMultiplayer;
            teleportManager.TeleportTo(questHolder.TargetItem.HandGrabInteractableCollector, teleportPos);
            
            if (_questHolders.All(questHolder => questHolder.IsComplete))
            {
                gameManager.Game.HasCompleteFirstFloor = true;
            }
        }

        public override void SaveSceneState()
        {
            SaveManager saveManager = SaveManager.Instance;
            if (saveManager == null)
            {
                return;
            }
            
            foreach (var questHolder in _questHolders)
            {
                saveManager.SaveQuestHolderState(questHolder);
            }
        }

        private void InitQuestHolders()
        {
            SaveManager saveManager = SaveManager.Instance;
            if (saveManager == null)
            {
                return;
            }

            foreach (var questHolder in _questHolders)
            {
                if (saveManager.TryGetSavedQuestHolderStatus(questHolder, out bool questHolderStatus))
                {
                    questHolder.Init(this, questHolderStatus);
                }
                else
                {
                    questHolder.Init(this, false);
                }
            }
        }
    }
}