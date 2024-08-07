using HarryPoter.Core;
using UnityEngine;

namespace Gameplay.Quests
{
    [RequireComponent(typeof(Collider))]
    public class TriggerQuest : Quest
    {
        [SerializeField] private string _tag;
        
        private bool _hasCompleted;
        
        private void OnTriggerEnter(Collider other)
        {
            TryDetectGameObjectWithTag(other.gameObject, _tag);
        }

        private void OnTriggerStay(Collider other)
        {
            TryDetectGameObjectWithTag(other.gameObject, _tag);
        }

        private void TryDetectGameObjectWithTag(GameObject gameObject, string tag)
        {
            if (_hasCompleted)
            {
                return;
            }

            if (!gameObject.CompareTag(tag))
            {
                return;
            }

            _hasCompleted = true;
            Complete();
        }
    }
}