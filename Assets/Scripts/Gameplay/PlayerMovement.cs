using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private float _speed = 8;
        
        private List<PlayerGestures.GestureConfig> _forwardConfigs;
        private List<PlayerGestures.GestureConfig> _backwardConfigs;

        private CharacterController _characterController;
        
        private bool _isMoving;
        private int _moveDirection;

        private void Awake()
        {
            _forwardConfigs = _player.PlayerGestures.GetConfigsByType(PlayerGestures.EGesture.Forward);
            _backwardConfigs = _player.PlayerGestures.GetConfigsByType(PlayerGestures.EGesture.Backward);

            _characterController = _player.CharacterController;
        }

        private void FixedUpdate()
        {
            foreach (var config in _forwardConfigs)
            {
                if (config.Group.Active)
                {
                    Forward();
                    return;
                }
            }

            foreach (var config in _backwardConfigs)
            {
                if (config.Group.Active)
                {
                    Backward();
                    return;
                }
            }
        }
        
        private void Forward()
        {
            Vector3 target = _player.CenterEye.forward * _speed / 10000 * Time.fixedTime;
            target.y = 0;

            _characterController.Move(target);
        }

        private void Backward()
        {
            Vector3 target = _player.CenterEye.forward * _speed / 10000 * Time.fixedTime;
            target.y = 0;

            _characterController.Move(target);
        }
    }
}