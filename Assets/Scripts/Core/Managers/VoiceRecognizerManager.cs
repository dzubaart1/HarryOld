using Mechaincs;
using Meta.WitAi.Json;
using Meta.WitAi.Requests;
using Oculus.Voice;
using UnityEngine;

namespace HarryPoter.Core
{
    public class VoiceRecognizerManager : MonoBehaviour
    {
        [SerializeField] private AppVoiceExperience _voiceExperience;

        private void OnEnable()
        {
            _voiceExperience.VoiceEvents.OnSend.AddListener(OnVoiceRequestStarted);
            _voiceExperience.VoiceEvents.OnResponse.AddListener(OnVoiceResponse);
            _voiceExperience.VoiceEvents.OnError.AddListener(OnVoiceError);
        }

        private void OnDisable()
        {
            _voiceExperience.VoiceEvents.OnSend.RemoveListener(OnVoiceRequestStarted);
            _voiceExperience.VoiceEvents.OnResponse.RemoveListener(OnVoiceResponse);
            _voiceExperience.VoiceEvents.OnError.RemoveListener(OnVoiceError);
        }

        public void StartVoiceRecognition()
        {
            if (!_voiceExperience.Active)
            {
                Debug.Log("Начало распознавания голоса...");
                _voiceExperience.Activate(); // Начало прослушивания   
            }
        }

        private void OnVoiceRequestStarted(VoiceServiceRequest request)
        {
            Debug.Log("Голосовой запрос отправлен...");
        }

        private void OnVoiceResponse(WitResponseNode response)
        {
            Debug.Log("Ответ Wit.ai: " + response.ToString());
            ProcessResponse(response);
        }

        private void OnVoiceError(string error, string message)
        {
            Debug.LogError($"Ошибка: {error} - {message}");
        }

        private void ProcessResponse(WitResponseNode response)
        {
            VoiceRecognizer[] voiceRecognizers = FindObjectsOfType<VoiceRecognizer>();
            foreach (var voiceRecognizer in voiceRecognizers)
            {
                if (voiceRecognizer.gameObject.activeSelf)
                {
                    voiceRecognizer.OnVoiceRecognized(response);
                }
            }
        }
    }
}