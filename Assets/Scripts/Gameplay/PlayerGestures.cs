using System;
using System.Collections.Generic;
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
            public ActiveStateGroup Group;
            public ActiveStateSelector Selector;
            public EGesture GestureType;
        }

        [SerializeField] private List<GestureConfig> _gestures = new List<GestureConfig>();
        
        public List<GestureConfig> GetGestureByType(EGesture gestureType)
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