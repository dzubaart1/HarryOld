using Oculus.Interaction;
using UnityEngine;

namespace HarryPoter.Core
{
    [RequireComponent(typeof(Grabbable))]
    public class PickedItem : MonoBehaviour
    {
        [SerializeField] private FindingItemsService.EListItem _listItem;

        private FindingItemsService _findingItemsService;
        private Grabbable _grabbable;
        
        private void Awake()
        {
            _grabbable = GetComponent<Grabbable>();
            _findingItemsService = Engine.GetService<FindingItemsService>();
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
            
            Debug.Log("PICK ITEM " + _listItem);
            _findingItemsService.CheckIn(_listItem);
        }
    }
}