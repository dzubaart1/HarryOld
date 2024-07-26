using System;
using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Player : MonoBehaviour
    {
        public enum EHand
        {
            Left,
            Right
        }

        public Hmd Hmd
        {
            get
            {
                return _hmd;
            }
        }

        [SerializeField] private Hmd _hmd;
        [SerializeField] private Hand _rightHand;
        [SerializeField] private Hand _leftHand;
        [SerializeField] private FingerFeatureStateProvider _leftFingerFeatures;
        [SerializeField] private FingerFeatureStateProvider _rightFingerFeatures;
        [SerializeField] private TransformFeatureStateProvider _leftTransformFeatures;
        [SerializeField] private TransformFeatureStateProvider _rightTransformFeatures;

        public Hand GetHandByType(EHand hand)
        {
            return hand switch
            {
                EHand.Left => _leftHand,
                EHand.Right => _rightHand,
                _ => _leftHand
            };
        }

        public FingerFeatureStateProvider GetFingerFeatureByType(EHand hand)
        {
            return hand switch
            {
                EHand.Left => _leftFingerFeatures,
                EHand.Right => _rightFingerFeatures,
                _ => _rightFingerFeatures
            };
        }
        
        public TransformFeatureStateProvider GetTransformFeatureByType(EHand hand)
        {
            return hand switch
            {
                EHand.Left => _leftTransformFeatures,
                EHand.Right => _rightTransformFeatures,
                _ => _leftTransformFeatures
            };
        }
    }
}