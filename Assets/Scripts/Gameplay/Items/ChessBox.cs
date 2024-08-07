using System;
using Gameplay.Quests;
using UnityEngine;

namespace HarryPoter.Core
{
    public class ChessBox : MonoBehaviour
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private TriggerQuest _triggerQuest;

        private void OnEnable()
        {
            _triggerQuest.OnCompleteEvent += OnComplete;
        }

        private void OnDisable()
        {
            _triggerQuest.OnCompleteEvent -= OnComplete;
        }

        public void OnComplete()
        {
            _animation.Play();
        }
    }
}