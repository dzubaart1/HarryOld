using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarryPoter.Core.LocalManagers.Interfaces;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace HarryPoter.Core.LocalManagers
{
    public class FirstFloorManager : BaseLocalManager
    {
        [Header("Refs")]
        [SerializeField] private HandGrabInteractableCollector _instructionHintPrefab;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _teleportPosForwardMultiplayer = 1f;
        [SerializeField] private bool _loadSceneAtEnd;
        [SerializeField] private string _sceneToLoadAtEnd;
        
        [Space]
        [SerializeField] private List<QuestHolder> _questHolders = new List<QuestHolder>();

        private bool _isCompleteFloor;
        
        private IEnumerator Start()
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                yield break;
            }

            yield return new WaitWhile(() => gameManager.GetPlayer() == null);
            
            gameManager.Game.StartGame();
            InitQuestHolders();
            InitInstructions();
        }

        private void Update()
        {
            if (_isCompleteFloor)
            {
                return;
            }
            
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }
            
            if (_loadSceneAtEnd && _questHolders.All(holder => holder.IsComplete && (holder.TargetItem == null || holder.TargetItem.IsCollected)))
            {
                gameManager.LoadScene(_sceneToLoadAtEnd);
                _isCompleteFloor = true;
            }
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
        }

        private void InitInstructions()
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

            HandGrabInteractableCollector instruction = Instantiate(_instructionHintPrefab, Vector3.zero, Quaternion.identity);
            
            Vector3 teleportPos = player.Head.position + player.Head.forward * _teleportPosForwardMultiplayer;

            teleportPos.y = 1f;
            teleportManager.TeleportTo(instruction, teleportPos);
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