using System;
using UnityEngine;

namespace HarryPoter.Core
{
    public class RuntimeBehaviour : MonoBehaviour
    {
        public event Action BehaviourUpdateEvent;
        public event Action BehaviourLateUpdateEvent;
        public event Action BehaviourDestroyEvent;
        
        private void Update()
        {
            BehaviourUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            BehaviourLateUpdateEvent?.Invoke();
        }

        private void OnDestroy()
        {
            BehaviourDestroyEvent?.Invoke();
        }
    }
}