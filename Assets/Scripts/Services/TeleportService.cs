using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class TeleportService : IService
    {
        private const float LERP_OFFSET = 0.2f;
        
        [CanBeNull] private GrabInteractable _currentObj;
        
        private ParticlesService _particlesService;
        private ParticleSystem _teleportParticles;
        
        private bool _isLerping;
        private float _lerpTimer;
        private Vector3 _topTarget;
        private Vector3 _bottomTarget;
        private Vector3 _currentTarget;
        
        public TeleportService(ParticlesService particlesService)
        {
            _particlesService = particlesService;
        }
        
        public void Initialize()
        {
            _teleportParticles = _particlesService.SpawnParticlesSystem(ParticlesConfiguration.EParticle.Teleport,Vector3.zero);
            
            Engine.Behaviour.BehaviourUpdateEvent += OnUpdate;
        }

        public void Destroy()
        {
            Engine.Behaviour.BehaviourUpdateEvent -= OnUpdate;
        }
        
        private void OnUpdate()
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

        public void Teleport(GrabInteractable obj, Transform target)
        {
            if (_currentObj != null)
            {
                return;
            }
            
            _currentObj = obj;

            StartLerp(target.position);
            _currentObj.transform.position = target.position;
            _currentObj.transform.rotation = target.rotation;

            _teleportParticles.transform.position = target.position;
            _teleportParticles.Play();
        }

        private void StartLerp(Vector3 originPos)
        {
            _isLerping = true;
            _topTarget = LERP_OFFSET * Vector3.up +  originPos;
            _bottomTarget = -LERP_OFFSET * Vector3.up + originPos;
            _currentTarget = _bottomTarget;
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
            
            _teleportParticles.Stop();
        }
    }
}