using Mechaincs;
using UnityEngine;
using UnityEngine.Serialization;

namespace HarryPoter.Core.Spells
{
    public class OpenSpell : SpellBase
    {
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

            Debug.Log($"FIND TARGET {status}");
            if (status)
            {
                target.OnActivateSpell(ESpell.Open);
            }
        }
    }
}