using Mechaincs;
using UnityEngine;

namespace HarryPoter.Core.Spells
{
    public class OpenSpell : SpellBase
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private TargetSpellFinder _targetSpellFinder;
        
        public override void StartSpell()
        {
            _targetSpellFinder.FindTarget(_layerMask, ESpell.Open, OnFindTarget);
        }

        private void OnFindTarget(bool status, SpellRecognizer target)
        {
            if (status)
            {
                target.OnActivateSpell(ESpell.Open);
            }
        }
    }
}