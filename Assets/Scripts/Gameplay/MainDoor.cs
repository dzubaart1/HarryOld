using System;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace HarryPoter.Core
{
    public class MainDoor : MonoBehaviour
    {
        [SerializeField] private GrabInteractableQuest _grabInteractableQuest;

        private GameCycleService _gameCycleService;
        
        private void Awake()
        {
            _gameCycleService = Engine.GetService<GameCycleService>();
        }

        private void OnEnable()
        {
            _grabInteractableQuest.OnCompleteEvent += OnQuestComplete;
        }

        private void OnDisable()
        {
            _grabInteractableQuest.OnCompleteEvent -= OnQuestComplete;
        }

        private void OnQuestComplete()
        {
            _gameCycleService.FinishGame();
        }
    }
}