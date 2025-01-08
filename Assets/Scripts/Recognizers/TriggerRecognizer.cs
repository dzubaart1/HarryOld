using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    public class TriggerRecognizer : MonoBehaviour
    {
        [SerializeField] private QuestHolder _questHolder;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"ENTER: {other.name}");
            _questHolder.TryCompleteTriggerQuest(this, other.transform, true);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"EXIT: {other.name}");
            _questHolder.TryCompleteTriggerQuest(this, other.transform, false);
        }
    }
}