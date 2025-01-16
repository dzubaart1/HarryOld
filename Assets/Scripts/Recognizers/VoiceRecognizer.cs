using HarryPoter.Core.Quests;
using Meta.WitAi.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mechaincs
{
    public class VoiceRecognizer : MonoBehaviour
    {
        [SerializeField] private string _targetEntity = "location";
        [SerializeField] private string _targetIntent = "teleport"; 
        [SerializeField] private QuestHolder _questHolder;

        public void OnVoiceRecognized(WitResponseNode response)
        {
            string text = response["text"].Value;
            Debug.Log("Распознанный текст: " + text);
            
            var intent = response["intents"][0]["name"].Value;
            if (intent == _targetIntent)
            {
                var value = response["entities"][$"{_targetEntity}:{_targetEntity}"][0]["value"].Value;
             
                _questHolder.TryCompleteVoiceQuest(this, _targetIntent, value);
            }
        }
    }
}