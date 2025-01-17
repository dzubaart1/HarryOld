using Mechaincs;
using UnityEngine;
using UnityEngine.Serialization;

namespace HarryPoter.Core.Spells
{
    public class OpenSpell : SpellBase
    {
        [SerializeField] private AudioSource _activateSpellSound;
        [SerializeField] private Transform _wandEnd;
        [SerializeField] private LayerMask _layerMask;
        [FormerlySerializedAs("_targetSpellFinder")] [SerializeField] private WandTargetFinder wandTargetFinder;
        
        public override void StartSpell()
        {
            IsSpelling = true;
            wandTargetFinder.FindTarget(_layerMask, ESpell.Open, OnFindTarget);
        }

        private void OnFindTarget(bool status, SpellRecognizer target)
        {
            IsSpelling = false;
            wandTargetFinder.Reset();

            if (status)
            {
                target.OnActivateSpell(ESpell.Open);
                _activateSpellSound.Play();
                ActivateEffect();
            }
        }
        
        private void ActivateEffect()
        {
            ParticlesManager particlesManager = ParticlesManager.Instance;
            if (particlesManager == null)
            {
                return;
            }
            
            if (!particlesManager.TryGetParticlesSystem(ParticlesManager.EParticle.ApplySpellEffect, out ParticleSystem particleSystem))
            {
                return;
            }

            particleSystem.transform.position = _wandEnd.position;
            particleSystem.Play();
        }
    }
}