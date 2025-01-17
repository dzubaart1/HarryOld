using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class ParticlesManager : MonoBehaviour
    {
        public enum EParticle
        {
            ApplySpellEffect,
            TeleportEffect,
            OpenSpellEffect,
            AttackWandSpellEffect,
            DisapearItemEffect,
            AttackTargetSpellEffect,
        }

        [Serializable]
        public class ParticlesSystemConfig
        {
            public EParticle Type;
            public ParticleSystem ParticleSystem;
        }
        
        [CanBeNull] public static ParticlesManager Instance { get; private set; }

        [SerializeField] private ParticlesSystemConfig[] _particlesSystemConfigs;

        [CanBeNull] private Transform _root;

        private Dictionary<EParticle, List<ParticleSystem>> _particlesPool = new Dictionary<EParticle, List<ParticleSystem>>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public bool TryGetParticlesSystem(EParticle particle, out ParticleSystem targetPS)
        {
            targetPS = null;

            if (!TryGetConfigByType(particle, out ParticlesSystemConfig targetConfig))
            {
                Debug.LogError("Can't find  config!");
                return false;
            }

            _particlesPool.TryGetValue(particle, out var list);

            if (list == null)
            {
                _particlesPool[particle] = new List<ParticleSystem>();
                list = _particlesPool[particle];
            }

            targetPS = list.FirstOrDefault(ps => ps != null && !ps.isPlaying);

            if (targetPS == null)
            {
                targetPS = Instantiate(targetConfig.ParticleSystem);
                list.Add(targetPS);
            }

            return true;
        }

        private bool TryGetConfigByType(EParticle particle, out ParticlesSystemConfig targetConfig)
        {
            targetConfig = null;

            foreach (var config in _particlesSystemConfigs)
            {
                if (config.Type == particle)
                {
                    targetConfig = config;
                    return true;
                }
            }

            return false;
        }
    }
}