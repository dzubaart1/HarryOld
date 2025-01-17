using System;
using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    public class SpellRecognizer : MonoBehaviour
    {
        public event Action<ESpell> OnSpellQuestComplete;

        [Header("Refs")]
        [SerializeField] private Transform _psSpawnPoint;
        [SerializeField] private QuestHolder _questHolder;
        
        public void OnActivateSpell(ESpell spell)
        {
            if (_questHolder.TryCompleteSpellQuest(this, spell))
            {
                ActivateEffects(spell);
                OnSpellQuestComplete?.Invoke(spell);   
            }
        }

        private void ActivateEffects(ESpell spell)
        {
            ParticlesManager particlesManager = ParticlesManager.Instance;
            if (particlesManager == null)
            {
                return;
            }

            ParticlesManager.EParticle targetParticle;
            switch (spell)
            {
                case ESpell.Open:
                    targetParticle = ParticlesManager.EParticle.OpenSpellEffect;
                    break;
                case ESpell.Attack:
                    targetParticle = ParticlesManager.EParticle.AttackTargetSpellEffect;
                    break;
                default:
                    return;
            }
            
            if (!particlesManager.TryGetParticlesSystem(targetParticle, out ParticleSystem targetPS))
            {
                return;
            }

            targetPS.transform.position = _psSpawnPoint.position;
            targetPS.Play();
        }
    }
}