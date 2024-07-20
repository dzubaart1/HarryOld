using System;
using System.Collections.Generic;
using Oculus.Interaction;

namespace HarryPoter.Core
{
    public class GestureConfiguration : Configuration
    {
        [Serializable]
        public struct GestureConfig
        {
            public ActiveStateSelector Selector;
            public GestureService.EGesture GestureType;
            public Player.EHand Hand;
        }
        
        public List<GestureConfig> Gestures;
    }
}