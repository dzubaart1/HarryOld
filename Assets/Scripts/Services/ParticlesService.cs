using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Quaternion = System.Numerics.Quaternion;

namespace HarryPoter.Core
{
    public class ParticlesService : IService
    {
        public ParticlesConfiguration Configuration { get; private set; }

        private Dictionary<ParticlesConfiguration.EParticle, List<ParticleSystem>> _particleSystems = new ();
        private Transform _root;

        public ParticlesService(ParticlesConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void Initialize()
        {
            _root = Engine.CreateObject("Partilces").transform;
        }

        public void Destroy()
        {
        }

        public ParticleSystem SpawnParticlesSystem(ParticlesConfiguration.EParticle particle, Vector3 pos)
        {
            _particleSystems.TryGetValue(particle, out var list);
            
            if(list == null)
            {
                _particleSystems[particle] = new List<ParticleSystem>();
                list = _particleSystems[particle];
            }

            ParticleSystem ps = list.FirstOrDefault(ps => !ps.isPlaying);

            if (ps == null)
            {
                ps = Object.Instantiate(GetConfigByType(particle).ParticleSystem);
                list.Add(ps);
            }
            
            ps.transform.position = pos;
            return ps;
        }

        private ParticlesConfiguration.ParticlesSystemConfig GetConfigByType(ParticlesConfiguration.EParticle particle)
        {
            foreach (var config in Configuration.ParticlesSystemConfigs)
            {
                if (config.Type == particle)
                {
                    return config;
                }
            }

            throw new ArgumentException("Can't find config!");
        }
    }
}