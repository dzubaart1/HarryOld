using UnityEngine;

namespace HarryPoter.Core
{
    public class Wand : MonoBehaviour
    {
        public WandDrawing WandDrawing
        {
            get
            {
                return _wandDrawing;
            }
        }

        public WandSpell WandSpell
        {
            get
            {
                return _wandSpell;
            }
        }
        
        [SerializeField] private WandDrawing _wandDrawing;
        [SerializeField] private WandSpell _wandSpell;
    }
}