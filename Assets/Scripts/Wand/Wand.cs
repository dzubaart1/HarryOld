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
        [SerializeField] private WandSpellGenerator _wandSpellGenerator;
        [SerializeField] private WandDrawing _wandDrawing;
        [SerializeField] private WandRecognizer _wandRecognizer;
        [SerializeField] private WandDebugger _wandDebugger;
        [SerializeField] private GrabInteractable _grabInteractable;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _noActiveLimitSecondsToFinishDrawing = 0.5f;
        [SerializeField] private float _deactivateDelay = 3f;

        [CanBeNull] private SpellBase _currentSpell;
        
        private float _deactivateTimer;
        private bool _isTimerActive;

        private void Update()
        {
            if (_isTimerActive)
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
            
            if (_grabInteractable.IsGrabbed & !_wandDrawing.IsDrawing)
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
            _grabInteractable.GrabEvent += OnGrab;
            _grabInteractable.UngrabEvent += OnUngrab;
        }

        private void OnDisable()
        {
            _grabInteractable.GrabEvent -= OnGrab;
            _grabInteractable.UngrabEvent -= OnUngrab;
        }
        
        private void OnGrab()
        {
            _isTimerActive = false;
        }

        private void OnUngrab()
        {
            _wandDebugger.ResetDebugDrawing();
            _wandDrawing.Reset();

            _isTimerActive = true;
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

            Player player = gameManager.CurrentLocalManager.GetPlayer();

            if (player == null)
            {
                return;
            }

            Vector3 planePoint = player.Head.position + player.Head.forward;
            planePoint.y = 0;

            Plane drawingPlane = new Plane(Vector3.up, planePoint);
            
            _wandDrawing.StartDrawing(drawingPlane);
        }

        private void OnFinishDrawing(List<Point> points)
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

            Player player = gameManager.CurrentLocalManager.GetPlayer();

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
                Vector3 planePoint = player.Head.position + player.Head.forward;
                planePoint.y = 0;
                
                _wandDebugger.DebugDrawing(points, planePoint);
                return;
            }
            
            if (!_wandRecognizer.TryRecognizeSpell(points, out SpellBase spell))
            {
                spell.StartSpell();
                _currentSpell = spell;
            }
        }
    }
}