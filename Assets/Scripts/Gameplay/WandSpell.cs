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

        [SerializeField] private List<SpellLayers> _layers;
        [SerializeField] private Transform _wandEnd;
        [SerializeField] private LineRenderer _selectLineRender;
        [SerializeField] private Material _wrongMaterial;
        [SerializeField] private Material _correctMaterial;
        [SerializeField] private Wand _wand;

        private ESpell _currentSpell = ESpell.None;
        private int _currentLayer;
        private float _focusingTime;
        private float _globalTime;

        private void Awake()
        {
            _selectLineRender.positionCount = 0;
        }

        private void Update()
        {
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
                    Reset();
                    ApplySpell(raycastHit.transform.gameObject);
                }
            }
            else
            {
                _focusingTime = 0;
                _selectLineRender.material = _wrongMaterial;
            }
        }

        public void ActivateSpell(ESpell spell)
        {
            _currentSpell = spell;
            _currentLayer = GetLayerMaskBySpell(spell).value;
            
            _wand.IsBusy = true;
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

            _wand.IsBusy = false;
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

        private void Reset()
        {
            _currentSpell = ESpell.None;
            _focusingTime = 0;
            _wand.IsBusy = false;
            _selectLineRender.positionCount = 0;
        }
    }
}