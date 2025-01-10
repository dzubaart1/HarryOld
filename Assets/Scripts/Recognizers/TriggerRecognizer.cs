using System.Collections.Generic;
using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    [RequireComponent(typeof(Collider))]
    public class TriggerRecognizer : MonoBehaviour
    {
        [SerializeField] private QuestHolder _questHolder;
        [SerializeField] private bool _isGetStack;
        [SerializeField] private Transform _stackPos;

        private List<Collider> _colliders = new List<Collider>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (_colliders.Contains(other))
            {
                return;
            }
            
            _colliders.Add(other);
            
            if (_questHolder.TryCompleteTriggerQuest(this, other.transform, true))
            {
                if (_isGetStack)
                {
                    HandGrabInteractableCollector collector = other.GetComponentInChildren<HandGrabInteractableCollector>();
                    if (collector == null)
                    {
                        return;
                    }
                
                    other.transform.rotation = Quaternion.identity;
                    other.transform.position = _stackPos.position;   
                    collector.ToggleGrabbing(false);
                }
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (_colliders.Contains(other))
            {
                return;
            }
            
            _colliders.Add(other);
            
            if (_questHolder.TryCompleteTriggerQuest(this, other.transform, true))
            {
                if (_isGetStack)
                {
                    HandGrabInteractableCollector collector = other.GetComponentInChildren<HandGrabInteractableCollector>();
                    if (collector == null)
                    {
                        return;
                    }
                
                    other.transform.rotation = Quaternion.identity;
                    other.transform.position = _stackPos.position;   
                    collector.ToggleGrabbing(false);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _colliders.Remove(other);
            
            if (_questHolder.TryCompleteTriggerQuest(this, other.transform, false))
            {
                
            }
        }
    }
}