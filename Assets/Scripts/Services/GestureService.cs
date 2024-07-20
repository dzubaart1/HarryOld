using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

namespace HarryPoter.Core
{
    public class GestureService : IService
    {
        public enum EGesture
        {
            Forward,
            Backward,
            Wand
        }
        
        public GestureConfiguration Configuration { get; private set; }

        public IReadOnlyCollection<GestureConfiguration.GestureConfig> Gestures
        {
            get
            {
                return _gestures;
            }
        }
        
        private List<GestureConfiguration.GestureConfig> _gestures = new List<GestureConfiguration.GestureConfig>();
        private Player _player;
        public GestureService(GestureConfiguration configuration, InputService inputService)
        {
            _player = inputService.Player;
            Configuration = configuration;
        }
        
        public Task Initialize()
        {
            foreach (var gesture in Configuration.Gestures)
            {
                GameObject obj = Engine.Instantiate(gesture.Selector.gameObject);
                
                HmdRef hmdRef = obj.GetComponent<HmdRef>();
                if (hmdRef != null)
                {
                    hmdRef.InjectHmd(_player.Hmd);
                }

                HandRef handRef = obj.GetComponent<HandRef>();
                if (handRef != null)
                {
                    handRef.InjectHand(_player.GetHandByType(gesture.Hand));
                }

                FingerFeatureStateProviderRef fingerFeatureStateProviderRef =
                    obj.GetComponent<FingerFeatureStateProviderRef>();
                if (fingerFeatureStateProviderRef != null)
                {
                    fingerFeatureStateProviderRef.InjectFingerFeatureStateProvider(_player.GetFingerFeatureByType(gesture.Hand));
                }

                TransformFeatureStateProviderRef transformFeatureStateProviderRef =
                    obj.GetComponent<TransformFeatureStateProviderRef>();
                if (transformFeatureStateProviderRef != null)
                {
                    transformFeatureStateProviderRef.InjectTransformFeatureStateProvider(_player.GetTransformFeatureByType(gesture.Hand));
                }
                
                _gestures.Add(new GestureConfiguration.GestureConfig()
                {
                    GestureType = gesture.GestureType,
                    Selector = obj.GetComponent<ActiveStateSelector>()
                });
            }
            
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }

        public List<GestureConfiguration.GestureConfig> GetConfigsByType(EGesture gestureType)
        {
            List<GestureConfiguration.GestureConfig> res = new List<GestureConfiguration.GestureConfig>();
            
            foreach (var gesture in _gestures)
            {
                if (gesture.GestureType == gestureType)
                {
                    res.Add(gesture);
                }
            }

            return res;
        }
    }
}