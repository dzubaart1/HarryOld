using System;
using System.Collections.Generic;
using HarryPoter.Core.Spells;
using JetBrains.Annotations;
using PDollarGestureRecognizer;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Wand : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private WandTargetFinder _wandTargetFinder;
        [SerializeField] private WandSpellGenerator _wandSpellGenerator;
        [SerializeField] private WandDrawing _wandDrawing;
        [SerializeField] private WandRecognizer _wandRecognizer;
        [SerializeField] private WandDebugger _wandDebugger;
        [SerializeField] private HandGrabInteractableCollector handGrabInteractableCollector;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _noActiveLimitSecondsToFinishDrawing = 0.5f;
        [SerializeField] private float _deactivateDelay = 3f;

        [CanBeNull] private SpellBase _currentSpell;
        
        private Plane _currentDrawingPlane;
        private float _deactivateTimer;
        private bool _isDeactivateTimerActive;
        
        private void Update()
        {
            if (_isDeactivateTimerActive)
            {
                _deactivateTimer -= Time.deltaTime;
                if (_deactivateTimer < 0)
                {
                    gameObject.SetActive(false);
                }   
            }
            
            if (_currentSpell != null && _currentSpell.IsSpelling)
            {
                return;
            }
            
            if (handGrabInteractableCollector.IsGrabbed & !_wandDrawing.IsDrawing)
            {
                OnStartDrawing();
            }

            if (_wandDrawing.LastAddedPointTime + _noActiveLimitSecondsToFinishDrawing < Time.time & _wandDrawing.IsDrawing)
            {
                List<Point> points = _wandDrawing.FinishDrawing();
                OnFinishDrawing(points);
            }
        }

        private void OnEnable()
        {
            MakeReset();
            
            handGrabInteractableCollector.GrabEvent += OnHandGrab;
            handGrabInteractableCollector.UngrabEvent += OnUngrab;
        }

        private void OnDisable()
        {
            MakeReset();
            
            handGrabInteractableCollector.GrabEvent -= OnHandGrab;
            handGrabInteractableCollector.UngrabEvent -= OnUngrab;
        }
        
        private void OnHandGrab()
        {
            _isDeactivateTimerActive = false;
        }

        private void OnUngrab()
        {
            MakeReset();
            
            _isDeactivateTimerActive = true;
            _deactivateTimer = _deactivateDelay;
        }

        private void OnStartDrawing()
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }
            
            if (gameManager.CurrentLocalManager == null)
            {
                return;
            }

            Player player = gameManager.GetPlayer();

            if (player == null)
            {
                return;
            }

            Vector3 headForward = player.Head.forward;
            headForward.y = 0;

            _currentDrawingPlane = new Plane(headForward, Vector3.zero);
            
            _wandDrawing.StartDrawing(_currentDrawingPlane);
        }

        private void OnFinishDrawing(List<Point> points)
        {
            _wandDrawing.Reset();
            
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }
            
            if (gameManager.CurrentLocalManager == null)
            {
                return;
            }

            Player player = gameManager.GetPlayer();

            if (player == null)
            {
                return;
            }
            
            if (_wandSpellGenerator.IsOn)
            {
                _wandSpellGenerator.WriteNewSpell(points);
                return;
            }

            if (_wandDebugger.IsOn)
            {
                _wandDebugger.DebugDrawing(points, _currentDrawingPlane);
            }
            
            if (_wandRecognizer.TryRecognizeSpell(points, out SpellBase spell))
            {
                _currentSpell = spell;
                spell.StartSpell();
            }
        }

        private void MakeReset()
        {
            _wandTargetFinder.Reset();
            _wandDebugger.Reset();
            _wandDrawing.Reset();
            
            _isDeactivateTimerActive = true;
            _deactivateTimer = _deactivateDelay;
        }
    }
}