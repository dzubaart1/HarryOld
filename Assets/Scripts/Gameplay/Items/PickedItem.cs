using System.Collections;
using Oculus.Interaction;
using UnityEngine;

namespace HarryPoter.Core
{
    [RequireComponent(typeof(Grabbable))]
    public class PickedItem : MonoBehaviour, ISpellable
    {
        [Header("Refs")]
        [SerializeField] private FindingItemsService.EListItem _listItem;
        [SerializeField] private ParticleSystem _pickedItemPartcles;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _disappearDelay = 3f;
        
        private FindingItemsService _findingItemsService;
        private Grabbable _grabbable;
        private Transform _wandTransform;
        
        private void Awake()
        {
            _grabbable = GetComponent<Grabbable>();
            _findingItemsService = Engine.GetService<FindingItemsService>();
            _wandTransform = Engine.GetService<WandService>().CurrentWand.transform;
        }

        private void OnEnable()
        {
            _grabbable.WhenPointerEventRaised += OnPoint;
        }

        private void OnDisable()
        {
            _grabbable.WhenPointerEventRaised -= OnPoint;
        }

        private void OnPoint(PointerEvent e)
        {
            if (e.Type != PointerEventType.Select)
            {
                return;
            }
            
            _findingItemsService.CheckIn(_listItem);

            StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(_disappearDelay);
            
            _pickedItemPartcles.Play();
            gameObject.SetActive(false);
        }

        public void OnOpenSpell()
        {
        }

        public void OnAttackSpell()
        {
        }

        public void OnTakeSpell()
        {
            transform.position = _wandTransform.position;
        }
    }
}