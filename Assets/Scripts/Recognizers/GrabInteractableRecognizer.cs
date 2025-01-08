using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mechaincs
{
    public class GrabInteractableRecognizer : MonoBehaviour
    {
        [SerializeField] private QuestHolder _questHolder;
        [FormerlySerializedAs("handGrabInteractable")] [SerializeField] private HandGrabInteractableCollector handGrabInteractableCollector;

        private void Update()
        {
            handGrabInteractableCollector.ToggleGrabbing(_questHolder.CurrentQuest is GrabInteractableQuest grabInteractableQuest);
        }

        private void OnEnable()
        {
            handGrabInteractableCollector.GrabEvent += OnHandGrab;
        }

        private void OnDisable()
        {
            handGrabInteractableCollector.GrabEvent -= OnHandGrab;
        }
        
        private void OnHandGrab()
        {
            _questHolder.TryCompleteGrabInteractableQuest();
        }
    }
}