using System;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace HarryPoter.Core
{
    public class GrabInteractable : MonoBehaviour
    {
        public event Action GrabEvent;
        public event Action UngrabEvent;
        
        [SerializeField] private List<HandGrabInteractable> _grabInteractables;

        public bool IsGrabbed = false;
        
        private void OnEnable()
        {
            foreach (var grabInteractable in _grabInteractables)
            {
                grabInteractable.WhenPointerEventRaised += OnGrabbed;
            }
        }

        private void OnDisable()
        {
            foreach (var grabInteractable in _grabInteractables)
            {
                grabInteractable.WhenPointerEventRaised -= OnGrabbed;
            }
        }

        public void ToggleGrabbing(bool isGrabEnabled)
        {
            foreach (var grabInteractable in _grabInteractables)
            {
                grabInteractable.enabled = isGrabEnabled;
            }
        }

        private void OnGrabbed(PointerEvent e)
        {
            if (e.Type == PointerEventType.Select)
            {
                IsGrabbed = true;
                GrabEvent?.Invoke();
            }

            if (e.Type == PointerEventType.Unselect)
            {
                IsGrabbed = false;
                UngrabEvent?.Invoke();
            }
        }
    }
}