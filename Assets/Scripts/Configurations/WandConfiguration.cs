using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandConfiguration : Configuration
    {
        public List<TextAsset> GestureTextAssets;
        public bool IsWritingNewSpells;
        public string SpellName;
        public Wand WandPrefab;
    }
}