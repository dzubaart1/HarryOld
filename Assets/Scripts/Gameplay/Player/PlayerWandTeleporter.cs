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

        private WandService _wandService;
        private List<PlayerGestures.GestureConfig> _wandGestures;

        private void Awake()
        {
            _wandService = Engine.GetService<WandService>();
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
            _wandService.TeleportWand(_spawnPoint.position, _spawnPoint.rotation);
        }
    }
}