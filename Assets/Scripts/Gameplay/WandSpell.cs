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

        private const float MIN_FOCUSING_TIME_TO_APPLY= 2f;

        [SerializeField] private List<SpellLayers> _layers;
        [SerializeField] private Transform _wandEnd;
        [SerializeField] private LineRenderer _selectLineRender;
        [SerializeField] private Material _wrongMaterial;
        [SerializeField] private Material _correctMaterial;

        private ESpell _currentSpell = ESpell.None;
        private int _currentLayer;
        private float _focusingTime;

        private void Awake()
        {
            _selectLineRender.enabled = false;
        }

        private void Update()
        {
            if (_currentSpell == ESpell.None)
            {
                return;
            }
            
            if (Physics.Raycast(_wandEnd.position, _wandEnd.transform.forward, out RaycastHit raycastHit))
            {
                if (raycastHit.transform.gameObject.layer != _currentLayer)
                {
                    _focusingTime = 0;
                    return;
                }

                _focusingTime += Time.deltaTime;
                _selectLineRender.material = _correctMaterial;
                
                if (_focusingTime > MIN_FOCUSING_TIME_TO_APPLY)
                {
                    _currentSpell = ESpell.None;
                    _focusingTime = 0;
                    ApplySpell(raycastHit.transform.gameObject);
                }
                
                return;
            }

            _selectLineRender.material = _wrongMaterial;
        }

        public void ActivateSpell(ESpell spell)
        {
            _currentSpell = spell;
            _currentLayer = GetLayerMaskBySpell(spell);
            _selectLineRender.enabled = true;
        }

        private void ApplySpell(GameObject gameObject)
        {
            switch (_currentSpell)
            {
                case ESpell.Attack:
                    AttackSpell();
                    break;
                case ESpell.Open:
                    OpenSpell();
                    break;
                case ESpell.Take:
                    TakeSpell();
                    break;
                default:
                    break;
            }

            _selectLineRender.enabled = false;
        }
        
        private void AttackSpell()
        {
            Debug.Log("ATTACK SPELL!");
        }

        private void TakeSpell()
        {
            Debug.Log("TAKE SPELL!");
        }

        private void OpenSpell()
        {
            Debug.Log("OPEN SPELL!");
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
    }
}