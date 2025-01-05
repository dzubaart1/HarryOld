using System;
using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    public class GrabInteractableRecognizer : MonoBehaviour
    {
        [SerializeField] private QuestHolder _questHolder;
        [SerializeField] private GrabInteractable _grabInteractable;

        private void Update()
        {
            _grabInteractable.ToggleGrabbing(_questHolder.CurrentQuest is GrabInteractableQuest grabInteractableQuest);
        }

        private void OnEnable()
        {
            _grabInteractable.GrabEvent += OnGrab;
        }

        private void OnDisable()
        {
            _grabInteractable.GrabEvent -= OnGrab;
        }
        
        private void OnGrab()
        {
            _questHolder.TryCompleteGrabInteractableQuest();
        }
    }
}