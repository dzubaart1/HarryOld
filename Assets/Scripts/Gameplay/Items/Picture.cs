using Oculus.Interaction;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Picture : MonoBehaviour
    {
        [SerializeField] private Grabbable _grabbable;
        [SerializeField] private Quest _quest;
        
        private void OnEnable()
        {
            _grabbable.WhenPointerEventRaised += OnSelected;
        }

        private void OnDisable()
        {
            _grabbable.WhenPointerEventRaised -= OnSelected;
        }

        private void OnSelected(PointerEvent e)
        {
            _quest.Complete();
        }
    }
}