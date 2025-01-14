using System.Collections.Generic;
using System.Linq;
using HarryPoter.Core.LocalManagers.Interfaces;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace HarryPoter.Core.LocalManagers
{
    public class SecondFloorManager : BaseLocalManager
    {
        [Header("Configs")]
        [SerializeField] private float _teleportPosForwardMultiplayer = 1f;
        [SerializeField] private bool _loadSceneAtEnd;
        [SerializeField] private string _sceneToLoadAtEnd;
        
        [Space]
        [SerializeField] private List<QuestHolder> _questHolders = new List<QuestHolder>();
        
        private void Start()
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
            
            TeleportManager teleportManager = TeleportManager.Instance;
            if (teleportManager == null)
            {
                return;
            }

            if (questHolder.HasTargetItem && questHolder.TargetItem != null)
            {
                questHolder.TargetItem.gameObject.SetActive(true);   
                Vector3 teleportPos = player.Head.position + player.Head.forward * _teleportPosForwardMultiplayer;
                teleportManager.TeleportTo(questHolder.TargetItem.HandGrabInteractableCollector, teleportPos);
            }
            
            if (_loadSceneAtEnd && _questHolders.All(holder => holder.IsComplete))
            {
                gameManager.LoadScene(_sceneToLoadAtEnd);
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