using System;
using System.Collections.Generic;
using HarryPoter.Core.LocalManagers.Interfaces;
using JetBrains.Annotations;
using Mechaincs;
using UnityEngine;

namespace HarryPoter.Core.Quests
{
    public class QuestHolder : MonoBehaviour
    {
        [SerializeField] private bool _hasTargetItem;
        [SerializeField] private TargetItem _targetItem;
        [SerializeField] private List<Quest> _quests = new List<Quest>();
        
        public bool IsComplete { get; private set; }

        public bool HasTargetItem => _hasTargetItem;
        [CanBeNull] public TargetItem TargetItem => _targetItem;

        [CanBeNull]
        public Quest CurrentQuest
        {
            get
            {
                if (_currentQuestID < 0 || _currentQuestID >= _quests.Count)
                {
                    return null;
                }

                return _quests[_currentQuestID];
            }
        }

        [CanBeNull] private BaseLocalManager _localManager;
        
        private int _currentQuestID;
        
        public bool TryCompleteGrabInteractableQuest()
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return false;
            }

            if (currentQuest is not GrabInteractableQuest)
            {
                return false;
            }

            if (_localManager == null)
            {
                return false;
            }

            if (_currentQuestID + 1 == _quests.Count & !IsComplete)
            {
                IsComplete = true;
                _localManager.OnQuestHolderCompleted(this);
            }
            else
            {
                _currentQuestID++;   
            }
            
            return true;
        }

        public bool TryCompleteTriggerQuest(TriggerRecognizer triggerRecognizer, Transform puttedObj, bool isEnter)
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return false;
            }

            if (currentQuest is not TriggerQuest triggerQuest)
            {
                return false;
            }

            if (isEnter != triggerQuest.IsEnter)
            {
                return false;
            }

            if (triggerRecognizer != triggerQuest.TriggerRecognizer)
            {
                return false;
            }

            if (puttedObj != triggerQuest.PuttedObj)
            {
                return false;
            }

            if (_localManager == null)
            {
                return false;
            }

            if (_currentQuestID + 1 == _quests.Count & !IsComplete)
            {
                IsComplete = true;
                _localManager.OnQuestHolderCompleted(this);
            }
            else
            {
                _currentQuestID++;   
            }
            
            return true;
        }

        public bool TryCompletePlayerPositionQuest(PlayerPositionRecognizer playerPositionRecognizer)
        {
            Quest currentQuest = CurrentQuest;
            if (currentQuest == null)
            {
                return false;
            }

            if (currentQuest is not PlayerPositionQuest playerPositionQuest)
            {
                return false;
            }

            if (playerPositionQuest.PositionRecognizer != playerPositionRecognizer)
            {
                return false;
            }
            
            if (_localManager == null)
            {
                return false;
            }

            if (_currentQuestID + 1 == _quests.Count & !IsComplete)
            {
                IsComplete = true;
                _localManager.OnQuestHolderCompleted(this);
            }
            else
            {
                _currentQuestID++;   
            }
            
            return true;
        }

        public bool TryCompleteSpellQuest(ESpell spellType)
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return false;
            }

            if (currentQuest is not SpellQuest spellQuest)
            {
                return false;
            }

            if (spellQuest.SpellType != spellType)
            {
                return false;
            }

            if (_localManager == null)
            {
                return false;
            }

            if (_currentQuestID + 1 == _quests.Count & !IsComplete)
            {
                IsComplete = true;
                _localManager.OnQuestHolderCompleted(this);
            }
            else
            {
                _currentQuestID++;   
            }
            
            return true;
        }

        public bool TryCompleteVoiceQuest(VoiceRecognizer voiceRecognizer, string intent, string value)
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return false;
            }

            if (currentQuest is not VoiceQuest voiceQuest)
            {
                return false;
            }

            if (voiceQuest.VoiceRecognizer != voiceRecognizer)
            {
                return false;
            }

            if (!voiceQuest.Intent.ToLower().Equals(intent.ToLower()) || !voiceQuest.Value.ToLower().Equals(value.ToLower()))
            {
                return false;
            }

            if (_localManager == null)
            {
                return false;
            }

            if (_currentQuestID + 1 == _quests.Count & !IsComplete)
            {
                IsComplete = true;
                _localManager.OnQuestHolderCompleted(this);
            }
            else
            {
                _currentQuestID++;   
            }
            
            return true;
        }

        public void Init(BaseLocalManager manager)
        {
            _localManager = manager;
            _currentQuestID = 0;

            if (_hasTargetItem && TargetItem != null)
            {
                TargetItem.gameObject.SetActive(false);   
            }
        }

        public void LoadState(bool isComplete)
        {
            IsComplete = isComplete;
        }
    }
}