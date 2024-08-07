using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private PlayerGestures _playerGestures;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _centerEye;

        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _speed = 8;
        
        private List<PlayerGestures.GestureConfig> _forwardConfigs;
        private List<PlayerGestures.GestureConfig> _backwardConfigs;
        
        private bool _isMoving;
        private int _moveDirection;

        private void Awake()
        {
            _forwardConfigs = _playerGestures.GetGestureByType(PlayerGestures.EGesture.Forward);
            _backwardConfigs = _playerGestures.GetGestureByType(PlayerGestures.EGesture.Backward);
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
            Vector3 target = _centerEye.forward * _speed / 10000 * Time.fixedTime;
            target.y = 0;

            _characterController.Move(target);
        }

        private void Backward()
        {
            Vector3 target = _centerEye.forward * _speed / 10000 * Time.fixedTime;
            target.y = 0;

            _characterController.Move(target);
        }
    }
}