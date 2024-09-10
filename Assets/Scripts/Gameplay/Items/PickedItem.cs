using System.Collections;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PickedItem : MonoBehaviour, ISpellable
    {
        [Header("Refs")]
        [SerializeField] private GrabInteractable _grabInteractable;
        
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _disappearDelay = 3f;
        [SerializeField] private FindingItemsService.EListItem _listItem;
        
        private FindingItemsService _findingItemsService;
        private ParticlesService _particlesService;
        private TeleportService _teleportService;
        
        private Transform _spawnPoint;
        
        private void Awake()
        {
            _particlesService = Engine.GetService<ParticlesService>();
            _findingItemsService = Engine.GetService<FindingItemsService>();
            _teleportService = Engine.GetService<TeleportService>();
            
            _spawnPoint = Engine.GetService<InputService>().Player.SpawnPoint;
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
            _findingItemsService.CheckIn(_listItem);

            StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(_disappearDelay);
            
            _particlesService.SpawnParticlesSystem(ParticlesConfiguration.EParticle.Disappear, transform.position).Play();
            gameObject.SetActive(false);
        }

        public void OnOpenSpell()
        {
        }

        public void OnAttackSpell()
        {
        }

        public void OnTakeSpell()
        {
            _teleportService.Teleport(_grabInteractable, _spawnPoint);
        }
    }
}