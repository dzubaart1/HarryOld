using System.Collections;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PickedItem : MonoBehaviour
    {
        /*[Header("Refs")]
        [SerializeField] private GrabInteractable _grabInteractable;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _disappearDelay = 3f;
        [SerializeField] private FindingItemsService.EListItem _listItem;
        
        private FindingItemsService _findingItemsService;
        private ParticlesPool _particlesPool;
        private PlayerObjectTeleport _playerObjectTeleport;
        
        private PlayerMovement player;
        
        private void Awake()
        {
            _particlesPool = Engine.GetService<ParticlesPool>();
            _findingItemsService = Engine.GetService<FindingItemsService>();
            _playerObjectTeleport = Engine.GetService<PlayerObjectTeleport>();
            
            player = Engine.GetService<InputService>().Player;
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
            
            _particlesPool.SpawnParticlesSystem(ParticlesConfiguration.EParticle.Disappear, transform.position).Play();
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
            _playerObjectTeleport.Teleport(_grabInteractable, player.SpawnPoint, Quaternion.identity);
        }*/
    }
}