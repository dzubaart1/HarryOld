using System;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class TeleportManager : MonoBehaviour
    {
        public static TeleportManager Instance { get; private set; }
        
        [Header("Configs")]
        [SerializeField] private float _lerpOffset = 0.2f;

        [Space]
        [Header("Refs")]
        [SerializeField] private ParticlesManager _particlesManager;
        
        [CanBeNull] private HandGrabInteractableCollector _currentObj;
        [CanBeNull] private ParticleSystem _teleportParticleSystem;
        
        private bool _isLerping;
        private float _lerpTimer;
        private Vector3 _topTarget;
        private Vector3 _bottomTarget;
        private Vector3 _currentTarget;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if ((_currentObj == null || _currentObj.IsGrabbed || !_currentObj.gameObject.activeSelf) & _isLerping)
            {
                StopLerp();
            }

            if (_isLerping)
            {
                UpdateLerp();
            }
        }

        public void TeleportTo(HandGrabInteractableCollector obj, Vector3 targetPos)
        {
            if (obj == null)
            {
                return;
            }
            
            if (_currentObj != null)
            {
                return;
            }

            if (!_particlesManager.TryGetParticlesSystem(ParticlesManager.EParticle.Teleport, out ParticleSystem teleportPS))
            {
                return;
            }

            _teleportParticleSystem = teleportPS;
            
            _currentObj = obj;
            _currentObj.transform.position = targetPos;
            _currentObj.transform.rotation = Quaternion.LookRotation(Vector3.up);
            
            StartLerp(_currentObj.transform.position);
        }

        private void StartLerp(Vector3 originPos)
        {
            _isLerping = true;
            _topTarget = _lerpOffset * Vector3.up +  originPos;
            _bottomTarget = -_lerpOffset * Vector3.up + originPos;
            _currentTarget = _bottomTarget;
            
            if (_teleportParticleSystem != null)
            {
                _teleportParticleSystem.transform.position = originPos;
                _teleportParticleSystem.Play();
            }
        }

        private void UpdateLerp()
        {
            if ((_currentTarget - _currentObj.transform.position).sqrMagnitude < 0.01f)
            {
                _currentTarget = _currentTarget == _topTarget ? _bottomTarget : _topTarget;
            }
            
            _currentObj.transform.position = Vector3.Lerp( _currentObj.transform.position, _currentTarget, Time.deltaTime);
        }

        private void StopLerp()
        {
            _currentObj = null;
            _isLerping = false;
            
            if (_teleportParticleSystem != null)
            {
                _teleportParticleSystem.Stop();

                _teleportParticleSystem = null;
            }
        }
    }
}