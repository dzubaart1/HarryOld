using Mechaincs;
using UnityEngine;

namespace HarryPoter.Core.Spells
{
    public class TakeSpell : SpellBase
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private TargetSpellFinder _targetSpellFinder;
        
        public override void StartSpell()
        {
            _targetSpellFinder.FindTarget(_layerMask, ESpell.Take, OnFindTarget);
        }

        private void OnFindTarget(bool status, SpellRecognizer target)
        {
            if (status)
            {
                target.OnActivateSpell(ESpell.Take);
            }
        }
    }
}