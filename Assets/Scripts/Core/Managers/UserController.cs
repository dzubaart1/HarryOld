using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class UserController : MonoBehaviour
    {
        [CanBeNull]
        public static UserController Instance { get; private set; }
        
        [Header("Configs")]
        [SerializeField] private float _teleportPosForwardMultiplayer = 1f;
        
        [Space]
        [Header("Refs")]
        [SerializeField] private VoiceRecognizerManager _voiceRecognizerManager;
        [SerializeField] private TeleportManager _teleportManager;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private WandSpawner _wandSpawner;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void SpawnWand()
        {
            _wandSpawner.SpawnWand();

            if (_wandSpawner.Wand == null)
            {
                return;
            }

            if (_playerSpawner.Player == null)
            {
                return;
            }

            GrabInteractable wand = _wandSpawner.Wand.GetComponentInChildren<GrabInteractable>();
            
            Vector3 teleportPos = _playerSpawner.Player.Head.position + _playerSpawner.Player.Head.forward * _teleportPosForwardMultiplayer;
            _teleportManager.TeleportTo(wand, teleportPos);
        }

        public void MoveForward()
        {
            if (_playerSpawner.Player == null)
            {
                return;
            }
            
            _playerSpawner.Player.MoveForward();
        }

        public void StopMoveForward()
        {
            if (_playerSpawner.Player == null)
            {
                return;
            }
            
            _playerSpawner.Player.StopMoveForward();
        }

        public void StartVoiceRecording()
        {
            _voiceRecognizerManager.StartVoiceRecognition();
        }
    }
}