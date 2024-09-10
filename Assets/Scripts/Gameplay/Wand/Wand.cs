using UnityEngine;

namespace HarryPoter.Core
{
    public class Wand : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private WandSpell _wandSpell;
        [SerializeField] private WandDrawing _wandDrawing;
        [SerializeField] private GrabInteractable _grabInteractable;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _deactivateDelay = 2f;
        public bool IsBusy;
        
        public WandSpell WandSpell => _wandSpell;
        public WandDrawing WandDrawing => _wandDrawing;
        public GrabInteractable GrabInteractable => _grabInteractable;

        private float _deactivateTimer;
        private bool _isTimerActive;

        private void Start()
        {
            Deactivate();
        }

        private void Update()
        {
            if (!_isTimerActive)
            {
                return;
            }
            
            _deactivateTimer -= Time.deltaTime;
            if (_deactivateTimer < 0 & !_grabInteractable.IsGrabbed)
            {
                Deactivate();
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
        
        private void Deactivate()
        {
            _isTimerActive = false;
            _deactivateTimer = _deactivateDelay;
            WandDrawing.Reset();
            WandSpell.Reset();
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            _isTimerActive = true;
            _deactivateTimer = _deactivateDelay;
            WandDrawing.Reset();
            WandSpell.Reset();
            gameObject.SetActive(true);
        }

        private void OnGrab()
        {
            _isTimerActive = false;
        }

        private void OnUngrab()
        {
            _isTimerActive = true;
            _deactivateTimer = _deactivateDelay;
        }
    }
}