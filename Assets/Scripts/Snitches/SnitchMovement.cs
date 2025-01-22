using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core.Snitches
{
    public class SnitchMovement : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private float _minDistance = 0.1f;
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private List<Transform> _points;
        
        private int _currentPoint;
        private void Start()
        {
            transform.position = _points[_currentPoint].position;
        }

        private void FixedUpdate()
        {
            if ((transform.position - _points[_currentPoint].position).sqrMagnitude < _minDistance)
            {
                _currentPoint = (_currentPoint + 1) % _points.Count;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, Time.fixedDeltaTime * _movementSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(_points[_currentPoint].position - transform.position), Time.fixedDeltaTime*_rotationSpeed);
        }
    }
}