using System;
using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    public class SpellRecognizer : MonoBehaviour
    {
        public event Action<ESpell> OnSpellQuestComplete;
        
        [SerializeField] private QuestHolder _questHolder;
        
        public void OnActivateSpell(ESpell spell)
        {
            _questHolder.TryCompleteSpellQuest(this, spell);
            OnSpellQuestComplete?.Invoke(spell);
        }
    }
}