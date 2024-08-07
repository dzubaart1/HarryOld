using UnityEngine;

namespace HarryPoter.Core
{
    public interface ISpellable
    {
        public void OnOpenSpell();
        public void OnAttackSpell();
        public void OnTakeSpell();
    }
}