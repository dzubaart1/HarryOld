using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class ParticlesConfiguration : Configuration
    {
        public enum EParticle
        {
            Disappear,
            QuestComplete,
            ApplySpell,
            Teleport
        }

        [Serializable]
        public struct ParticlesSystemConfig
        {
            public EParticle Type;
            public ParticleSystem ParticleSystem;
        }

        public List<ParticlesSystemConfig> ParticlesSystemConfigs;
    }
}