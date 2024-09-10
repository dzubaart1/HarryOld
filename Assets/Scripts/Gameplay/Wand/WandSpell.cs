using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandSpell : MonoBehaviour
    {
        public enum ESpell
        {
            Attack,
            Take,
            Open,
            None
        }

        [Serializable]
        private struct SpellLayers
        {
            public ESpell SpellType;
            public LayerMask LayerMask;
        }

        private const float MIN_FOCUSING_TIME_TO_APPLY= 1.5f;
        private const float SPELL_TIME = 4f;
        
        [Header("Refs")]
        [SerializeField] private List<SpellLayers> _layers;
        [SerializeField] private Transform _wandEnd;
        [SerializeField] private Wand _wand;
        
        [Space]
        [Header("Views")]
        [SerializeField] private LineRenderer _selectLineRender;
        [SerializeField] private Material _attackMaterial;
        [SerializeField] private Material _takeMaterial;
        [SerializeField] private Material _openMaterial;
        [SerializeField] private Material _correctMaterial;

        private ParticlesService _particlesService;
        
        private ESpell _currentSpell = ESpell.None;
        private Material _currentMaterial;
        private int _currentLayer;
        private float _focusingTime;
        private float _globalTime;

        private void Awake()
        {
            _selectLineRender.positionCount = 0;

            _particlesService = Engine.GetService<ParticlesService>();
        }

        private void Update()
        {
            if (!_wand.GrabInteractable.IsGrabbed)
            {
                return;
            }
            
            if (_currentSpell == ESpell.None)
            {
                return;
            }

            _selectLineRender.positionCount = 2;
            _selectLineRender.SetPosition(0, _wandEnd.position);
            _selectLineRender.SetPosition(1, _wandEnd.position + _wandEnd.forward*3);

            if (_focusingTime == 0)
            {
                _globalTime += Time.deltaTime;
            }

            if (_globalTime > SPELL_TIME)
            {
                _globalTime = 0;
                Reset();
            }

            Ray ray = new Ray(_wandEnd.position, _wandEnd.forward);
            
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _currentLayer))
            {
                _focusingTime += Time.deltaTime;
                _selectLineRender.material = _correctMaterial;
                
                if (_focusingTime > MIN_FOCUSING_TIME_TO_APPLY)
                {
                    ApplySpell(raycastHit.transform.gameObject);
                }
            }
            else
            {
                _focusingTime = 0;
                _selectLineRender.material = _currentMaterial;
            }
        }

        public void ActivateSpell(ESpell spell)
        {
            _currentSpell = spell;
            _currentLayer = GetLayerMaskBySpell(spell).value;
            _currentMaterial = GetMaterialBySpell(spell);
            
            _wand.IsBusy = true;
        }
        
        public void Reset()
        {
            _currentSpell = ESpell.None;
            _focusingTime = 0;
            _wand.IsBusy = false;
            _selectLineRender.positionCount = 0;
        }

        private void ApplySpell(GameObject gameObject)
        {
            ISpellable spellable = gameObject.GetComponentInChildren<ISpellable>();
            if (spellable == null)
            {
                Debug.Log("Can't find ISpellable!");
                return;
            }
            
            switch (_currentSpell)
            {
                case ESpell.Attack:
                    spellable.OnAttackSpell();
                    break;
                case ESpell.Open:
                    spellable.OnOpenSpell();
                    break;
                case ESpell.Take:
                    spellable.OnTakeSpell();
                    break;
                default:
                    break;
            }
            
            _particlesService.SpawnParticlesSystem(ParticlesConfiguration.EParticle.ApplySpell, _wandEnd.position).Play();
            Reset();
        }

        private LayerMask GetLayerMaskBySpell(ESpell spell)
        {
            foreach (var spellLayer in _layers)
            {
                if (spellLayer.SpellType == spell)
                {
                    return spellLayer.LayerMask;
                }
            }

            throw new ArgumentException("Can't Find Layer By Spell");
        }

        private Material GetMaterialBySpell(ESpell spell)
        {
            switch (spell)
            {
                case ESpell.Attack:
                    return _attackMaterial;
                case ESpell.Open:
                    return _openMaterial;
                case ESpell.Take:
                    return _takeMaterial;
                default:
                    return _attackMaterial;
            }
        }
    }
}