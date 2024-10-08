using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private PlayerGestures _playerGestures;
        [SerializeField] private Transform _rotationPoint;
        [SerializeField] private Transform _movePoint;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _speed = 8;
        
        private List<PlayerGestures.GestureConfig> _forwardConfigs;
        private List<PlayerGestures.GestureConfig> _backwardConfigs;

        private Vector3 _moveOffset;
        
        private void Awake()
        {
            _forwardConfigs = _playerGestures.GetGestureByType(PlayerGestures.EGesture.Forward);
            _backwardConfigs = _playerGestures.GetGestureByType(PlayerGestures.EGesture.Backward);
        }

        private void Update()
        {
            _moveOffset = Vector3.zero;
            
            _movePoint.rotation = Quaternion.Euler(0, _rotationPoint.rotation.eulerAngles.y, 0);
            _movePoint.position = transform.position + _movePoint.forward;
            
            foreach (var config in _forwardConfigs)
            {
                if (config.Group.Active)
                {
                    Vector3 direction = (_movePoint.position- transform.position).normalized;
                    direction.y = 0;

                    _moveOffset = direction * _speed / 100;
                    return;
                }
            }

            foreach (var config in _backwardConfigs)
            {
                if (config.Group.Active)
                {
                    Vector3 direction = (_movePoint.position- transform.position).normalized;
                    direction.y = 0;

                    _moveOffset = direction * _speed / 100;
                    return;
                }
            }
        }

        private void FixedUpdate()
        {
            transform.position += _moveOffset;
        }
    }
}