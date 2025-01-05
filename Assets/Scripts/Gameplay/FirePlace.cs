using UnityEngine;

namespace HarryPoter.Core
{
    public class FirePlace : MonoBehaviour
    {
        /*[SerializeField] private GameObject _resultBox;
        
        private PlayerVoiceRecognizer _playerVoiceRecognizer;
        private ParticlesPool _particlesPool;
        private GameCycleService _gameCycleService;
        
        private bool _isPlayerInTrigger;
        
        private void Awake()
        {
            _gameCycleService = Engine.GetService<GameCycleService>();
            _particlesPool = Engine.GetService<ParticlesPool>();
            
            _playerVoiceRecognizer = Engine.GetService<InputService>().Player
                .GetComponentInChildren<PlayerVoiceRecognizer>();
        }

        private void OnEnable()
        {
            _playerVoiceRecognizer.RecognizedTeleportingEvent += OnRecognizedTeleporting;
        }

        private void OnDisable()
        {
            _playerVoiceRecognizer.RecognizedTeleportingEvent -= OnRecognizedTeleporting;
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponentInChildren<Player>();

            if (player == null)
            {
                return;
            }

            _isPlayerInTrigger = true;
        }
        
        private void OnTriggerStay(Collider other)
        {
            Player player = other.GetComponentInChildren<Player>();

            if (player == null)
            {
                return;
            }

            _isPlayerInTrigger = true;
        }
        
        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponentInChildren<Player>();

            if (player == null)
            {
                return;
            }

            _isPlayerInTrigger = false;
        }

        private void OnRecognizedTeleporting()
        {
            if (!_isPlayerInTrigger)
            {
                return;
            }

            _particlesPool.SpawnParticlesSystem(ParticlesConfiguration.EParticle.QuestComplete, _resultBox.transform.position);
            _resultBox.gameObject.SetActive(false);
            _gameCycleService.AllItemsFound();
        }*/
    }
}