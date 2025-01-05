using UnityEngine;

namespace HarryPoter.Core.Spells
{
    public abstract class SpellBase : MonoBehaviour
    {
        [SerializeField] private string _gestureClass;
        [SerializeField] private ESpell _spellType;

        public string GestureClass => _gestureClass;
        public ESpell SpellType => _spellType;
        public bool IsSpelling { get; protected set; }
        
        public abstract void StartSpell();
    }
}