using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerWandTeleporter : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerGestures _playerGestures;

        private TeleportService _teleportService;
        private WandService _wandService;
        private List<PlayerGestures.GestureConfig> _wandGestures;

        private void Awake()
        {
            _wandService = Engine.GetService<WandService>();
            _teleportService = Engine.GetService<TeleportService>();
            _wandGestures = _playerGestures.GetGestureByType(PlayerGestures.EGesture.Wand);
        }

        private void OnEnable()
        {
            foreach (var wandGesture in _wandGestures)
            {
                wandGesture.Selector.WhenSelected += TeleportWand;
            }
        }

        private void OnDisable()
        {
            foreach (var wandGesture in _wandGestures)
            {
                wandGesture.Selector.WhenSelected -= TeleportWand;
            }
        }

        private void TeleportWand()
        {
            _teleportService.Teleport(_wandService.CurrentWand.GrabInteractable, _spawnPoint);
            _wandService.CurrentWand.Activate();
        }
    }
}