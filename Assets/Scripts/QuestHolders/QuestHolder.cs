using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core.Quests
{
    public class QuestHolder : MonoBehaviour
    {
        public event Action QuestHolderCompleteEvent;
        
        [SerializeField] private EListItem _listItem;
        [SerializeField] private List<Quest> _quests = new List<Quest>();
        [SerializeField] private int _questHolderID;
        
        public EListItem ListItem => _listItem;
        public bool IsComplete { get; private set; }
        public int QuestHolderID => _questHolderID;

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

        [CanBeNull] private QuestHoldersManager _questHoldersManager;
        
        private int _currentQuestID;
        
        public void TryCompleteGrabInteractableQuest()
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return;
            }

            if (currentQuest is not GrabInteractableQuest grabInteractableQuest)
            {
                return;
            }

            if (_questHoldersManager == null)
            {
                return;
            }

            if (_currentQuestID + 1 == _quests.Count)
            {
                IsComplete = true;
                _questHoldersManager.OnQuestHolderComplete(this);
                return;
            }
            
            _currentQuestID++;
        }

        public void TryCompleteSpellQuest(ESpell spellType)
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return;
            }

            if (currentQuest is not SpellQuest spellQuest)
            {
                return;
            }

            if (spellQuest.SpellType != spellType)
            {
                return;
            }

            if (_questHoldersManager == null)
            {
                return;
            }

            if (_currentQuestID + 1 == _quests.Count)
            {
                IsComplete = true;
                _questHoldersManager.OnQuestHolderComplete(this);
                return;
            }
            
            _currentQuestID++;
        }

        public void TryCompleteVoiceQuest(string voiceText)
        {
            Quest currentQuest = CurrentQuest;
            
            if (currentQuest == null)
            {
                return;
            }

            if (currentQuest is not VoiceQuest voiceQuest)
            {
                return;
            }

            if (!voiceQuest.VoiceText.Equals(voiceText))
            {
                return;
            }

            if (_questHoldersManager == null)
            {
                return;
            }

            if (_currentQuestID + 1 == _quests.Count)
            {
                IsComplete = true;
                _questHoldersManager.OnQuestHolderComplete(this);
                return;
            }
            
            _currentQuestID++;
        }

        public void Init(QuestHoldersManager manager, bool hasCompleteQuestHolder)
        {
            _questHoldersManager = manager;
            _currentQuestID = 0;

            IsComplete = hasCompleteQuestHolder;
        }
    }
}