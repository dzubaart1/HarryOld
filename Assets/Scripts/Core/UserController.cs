using System;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class UserController : MonoBehaviour
    {
        [CanBeNull] public static UserController Instance { get; private set; }
        
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private WandSpawner _wandSpawner;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void TeleportWand()
        {
            
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
            if (_playerSpawner.Player == null)
            {
                return;
            }
            
            _playerSpawner.Player.StartVoiceRecording();
        }

    }
}