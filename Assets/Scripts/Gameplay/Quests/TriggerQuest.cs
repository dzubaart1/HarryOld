using System;
using HarryPoter.Core;
using UnityEngine;

namespace Gameplay.Quests
{
    [RequireComponent(typeof(Collider))]
    public class TriggerQuest : Quest
    {
        public event Action<Transform> OnRookEnterEvent;
        
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

        private void TryDetectGameObjectWithTag(GameObject go, string tag)
        {
            if (_hasCompleted)
            {
                return;
            }

            if (!go.CompareTag(tag))
            {
                return;
            }

            _hasCompleted = true;
            OnRookEnterEvent?.Invoke(go.transform);
            Complete();
        }
    }
}