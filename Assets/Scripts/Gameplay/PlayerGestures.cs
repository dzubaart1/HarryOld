using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerGestures : MonoBehaviour
    {
        public enum EGesture
        {
            Forward,
            Backward,
            Wand
        }
        
        [Serializable]
        public struct GestureConfig
        {
            [HideInInspector] public ActiveStateGroup Group;
            public ActiveStateSelector Selector;
            public EGesture GestureType;
            public Player.EHand Hand;
        }

        [SerializeField] private List<GestureConfig> _gesturePrefabs;
        [SerializeField] private Player _player;
        
        private List<GestureConfig> _gestures = new List<GestureConfig>();

        private void Awake()
        {
            foreach (var gesture in _gesturePrefabs)
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
                
                _gestures.Add(new GestureConfig()
                {
                    GestureType = gesture.GestureType,
                    Selector = obj.GetComponent<ActiveStateSelector>(),
                    Group = obj.GetComponent<ActiveStateGroup>()
                });
            }
        }
        
        public List<GestureConfig> GetConfigsByType(EGesture gestureType)
        {
            List<GestureConfig> res = new List<GestureConfig>();
            
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