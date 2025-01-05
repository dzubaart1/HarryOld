using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerObjectTeleport : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private float _lerpOffset = 0.2f;
        
        [Space]
        [Header("Refs")] 
        [SerializeField] private ParticlesPool _partilcesPool;

        [CanBeNull] public PlayerObjectTeleport Instance { get; private set; }
        
        
        [CanBeNull] private GrabInteractable _currentObj;
        [CanBeNull] private ParticleSystem _currentParticleSystem;
        
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

        public void Teleport(GrabInteractable obj)
        {
            if (obj == null)
            {
                return;
            }
            
            if (_currentObj != null)
            {
                return;
            }

            if (_partilcesPool.TryGetParticlesSystem(ParticlesPool.EParticle.Teleport, out ParticleSystem teleportPS))
            {
                return;
            }

            _currentParticleSystem = teleportPS;
            
            _currentObj = obj;
            _currentObj.transform.position = obj.transform.position;
            _currentObj.transform.rotation = obj.transform.rotation;
            
            StartLerp(obj.transform.position);
        }

        private void StartLerp(Vector3 originPos)
        {
            _isLerping = true;
            _topTarget = _lerpOffset * Vector3.up +  originPos;
            _bottomTarget = -_lerpOffset * Vector3.up + originPos;
            _currentTarget = _bottomTarget;
            
            if (_currentParticleSystem != null)
            {
                _currentParticleSystem.transform.position = originPos;
                _currentParticleSystem.Play();
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
            
            if (_currentParticleSystem != null)
            {
                _currentParticleSystem.Stop();

                _currentParticleSystem = null;
            }
        }
    }
}