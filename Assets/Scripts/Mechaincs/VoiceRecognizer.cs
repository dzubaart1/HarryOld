using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    public class VoiceRecognizer : MonoBehaviour
    {
        [SerializeField] private QuestHolder _questHolder;

        public void OnVoiceRecognized(string text)
        {
            _questHolder.TryCompleteVoiceQuest(text);
        }
    }
}