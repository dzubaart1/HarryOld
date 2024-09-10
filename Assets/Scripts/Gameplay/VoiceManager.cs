using Meta.WitAi;
using Meta.WitAi.Json;
using Oculus.Voice;
using UnityEngine;

namespace HarryPoter.Core
{
    public class VoiceManager : MonoBehaviour
    {
        [SerializeField] private AppVoiceExperience _appVoiceExperience;
        
        private bool _activated;

        private void OnEnable()
        {
            _appVoiceExperience.VoiceEvents.OnFullTranscription.AddListener(OnFullTranscription);
            _appVoiceExperience.VoiceEvents.OnResponse.AddListener(OnResponse);
        }

        private void OnDisable()
        {
            _appVoiceExperience.VoiceEvents.OnFullTranscription.RemoveListener(OnFullTranscription);
            _appVoiceExperience.VoiceEvents.OnResponse.RemoveListener(OnResponse);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) & _appVoiceExperience)
            {
                Debug.Log("Space Activate!");
                _appVoiceExperience.Activate();
                _activated = true;
            }
        }
        
        private void OnFullTranscription(string text)
        {
            Debug.Log("Full Transcription! " + text);
        }

        private void OnResponse(WitResponseNode responseNode)
        {
            string[] place = responseNode.GetAllEntityValues("place:place");
            WitResponseNode value = responseNode["intents"];
            string[] intents = value.ChildNodeNames;
        }
    }
}