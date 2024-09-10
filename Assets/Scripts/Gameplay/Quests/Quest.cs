using System;
using UnityEngine;

namespace HarryPoter.Core
{
    public abstract class Quest : MonoBehaviour
    {
        public event Action OnCompleteEvent;
        
        [Header("Refs")]
        [SerializeField] protected GrabInteractable _gift;
        [SerializeField] private Transform _particlesSystemPos;

        [Space]
        [Header("Configs")]
        [SerializeField] private bool _isTeleportToPlayer;
        
        private ParticlesService _particlesService;
        private TeleportService _teleportService;
        private Transform _objectSpawnPoint;

        private void Awake()
        {
            _gift.gameObject.SetActive(false);

            _objectSpawnPoint = Engine.GetService<InputService>().Player.SpawnPoint;
            _teleportService = Engine.GetService<TeleportService>();
            _particlesService = Engine.GetService<ParticlesService>();
        }

        protected void Complete()
        {
            OnCompleteEvent?.Invoke();
            _particlesService.SpawnParticlesSystem(ParticlesConfiguration.EParticle.QuestComplete, _particlesSystemPos.position).Play();
            _gift.gameObject.SetActive(true);

            if (_isTeleportToPlayer)
            {
                _teleportService.Teleport(_gift, _objectSpawnPoint);
            }
        }
    }
}