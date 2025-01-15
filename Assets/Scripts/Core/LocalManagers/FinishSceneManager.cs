using System.Collections.Generic;
using System.Linq;
using HarryPoter.Core;
using HarryPoter.Core.LocalManagers.Interfaces;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace Core.LocalManagers
{
    public class FinishSceneManager : BaseLocalManager
    {
        [Header("Refs")]
        [SerializeField] private ResultsUICntrl _resultsUICntrlPrefab;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _teleportPosForwardMultiplayer = 1f;
        
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
            
            if (!gameManager.Game.GameFinished && _questHolders.All(holder => holder.IsComplete))
            {
                gameManager.Game.FinishGame();
                OnAllQuestHoldersComplete();
            }
        }

        private void OnAllQuestHoldersComplete()
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
            
            ResultsUICntrl resultsUICntrl = Instantiate(_resultsUICntrlPrefab, Vector3.zero,Quaternion.identity);
            resultsUICntrl.SetGameTime(gameManager.Game.FinishGameTime - gameManager.Game.StartGameTime);
            
            Vector3 teleportPos = player.Head.position + player.Head.forward * _teleportPosForwardMultiplayer;
            teleportManager.TeleportTo(resultsUICntrl.GrabInteractableCollector, teleportPos);
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