using System;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace HarryPoter.Core.Quests
{
    public class HandGrabInteractableQuest : Quest
    {
        [SerializeField] private HandGrabInteractable _handGrabInteractable;

        private bool _hasCompleted;

        private void OnEnable()
        {
            _handGrabInteractable.WhenPointerEventRaised += OnPointerRaised;
        }

        private void OnDisable()
        {
            _handGrabInteractable.WhenPointerEventRaised -= OnPointerRaised;
        }

        private void OnPointerRaised(PointerEvent pointerEvent)
        {
            if (_hasCompleted)
            {
                return;
            }

            _hasCompleted = true;
            Complete();
        }
    }
}