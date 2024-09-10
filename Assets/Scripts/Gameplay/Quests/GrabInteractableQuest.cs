using UnityEngine;

namespace HarryPoter.Core.Quests
{
    public class GrabInteractableQuest : Quest
    {
        [SerializeField] private GrabInteractable _grabInteractable;

        private bool _hasCompleted;

        private void OnEnable()
        {
            _grabInteractable.GrabEvent += OnGrab;
        }

        private void OnDisable()
        {
            _grabInteractable.GrabEvent -= OnGrab;
        }

        private void OnGrab()
        {
            if (_hasCompleted)
            {
                return;
            }

            _hasCompleted = true;
            Complete();
        }
    }
}