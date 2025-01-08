using System;
using JetBrains.Annotations;
using Mechaincs;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandTargetFinder : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _wandEnd;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _focusingTime = 1.5f;
        [SerializeField] private float _outFocusingTime = 4f;
        [SerializeField] private float _lineRendererLength = 3f;

        [Space]
        [Header("Views")]
        [SerializeField] private Material _wrongTargetMaterial;
        [SerializeField] private Material _correctTargetMaterial;

        [CanBeNull] private Transform _target;
        [CanBeNull] private Action<bool, SpellRecognizer> _onTargetFind;
        
        private bool _isTargeting;
        private float _focusingTimer;
        private float _outFocusingTimer;

        private ESpell _currentSpell;
        private LayerMask _targetLayerMask;
        
        private void Update()
        {
            if (!_isTargeting)
            {
                return;
            }

            if (_outFocusingTimer > _outFocusingTime)
            {
                _isTargeting = false;
                _onTargetFind?.Invoke(false, null);
                return;
            }

            if (_focusingTimer > _focusingTime)
            {
                if (_target == null)
                {
                    return;
                }
                
                _isTargeting = false;
                _onTargetFind?.Invoke(true, _target.GetComponentInChildren<SpellRecognizer>());
                return;
            }

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _wandEnd.position);
            _lineRenderer.SetPosition(1, _wandEnd.position + _wandEnd.forward * _lineRendererLength);
            
            Ray ray = new Ray(_wandEnd.position, _wandEnd.forward);
            
            if (!Physics.Raycast(ray, out RaycastHit raycastHit, _lineRendererLength, _targetLayerMask))
            {
                _outFocusingTimer += Time.deltaTime;
                _focusingTimer = 0;
                _lineRenderer.material = _wrongTargetMaterial;
                return;
            }
            
            SpellRecognizer spellRecognizer = raycastHit.transform.GetComponentInChildren<SpellRecognizer>();
            if (spellRecognizer == null)
            {
                _outFocusingTimer += Time.deltaTime;
                _focusingTimer = 0;
                _lineRenderer.material = _wrongTargetMaterial;
                return;
            }

            Transform currentTarget = raycastHit.transform;

            if (currentTarget == _target)
            {
                _outFocusingTimer = 0f;
                _focusingTimer += Time.deltaTime;
                
                _lineRenderer.material = _correctTargetMaterial;
                return;    
            }

            if (currentTarget != _target)
            {
                _outFocusingTimer = 0f;
                _focusingTimer = 0f;
                
                _lineRenderer.material = _correctTargetMaterial;
                _target = currentTarget;
                return;
            }
        }

        public void FindTarget(LayerMask targetLayerMask, ESpell spell, Action<bool, SpellRecognizer> onTargetFind)
        {
            MakeReset();
            
            _currentSpell = spell;
            _targetLayerMask = targetLayerMask;
            _onTargetFind = onTargetFind;
            
            _isTargeting = true;
        }

        public void Reset()
        {
            MakeReset();
        }

        private void MakeReset()
        {
            _lineRenderer.positionCount = 0;
            _outFocusingTimer = 0f;
            _focusingTimer = 0f;
        }
    }
}