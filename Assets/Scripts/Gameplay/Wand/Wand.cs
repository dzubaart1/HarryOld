using System;
using System.Collections.Generic;
using System.Threading;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Wand : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private List<HandGrabInteractable> _grabInteractables;
        [SerializeField] private WandSpell _wandSpell;
        [SerializeField] private WandDrawing _wandDrawing;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _timer = 2f;
        public bool IsBusy;
        
        public bool IsGrabbed { get; private set; }
        public WandSpell WandSpell => _wandSpell;
        public WandDrawing WandDrawing => _wandDrawing;

        private float _deactivateTimer;

        private void Update()
        {
            if (IsGrabbed)
            {
                _deactivateTimer = _timer;
                return;
            }

            _deactivateTimer -= Time.deltaTime;
            if (_deactivateTimer < 0)
            {
                Deactivate();
                _deactivateTimer = _timer;
            }
        }

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

        private void OnGrabbed(PointerEvent e)
        {
            if (e.Type == PointerEventType.Select)
            {
                IsGrabbed = true;
            }

            if (e.Type == PointerEventType.Unselect)
            {
                Deactivate();
            }
        }

        public void Deactivate()
        {
            _deactivateTimer = _timer;
            IsGrabbed = false;
            WandDrawing.Reset();
            WandSpell.Reset();
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            _deactivateTimer = _timer;
            IsGrabbed = false;
            WandDrawing.Reset();
            WandSpell.Reset();
            gameObject.SetActive(true);
        }
    }
}