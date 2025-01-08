using System.Collections.Generic;
using HarryPoter.Core.Spells;
using JetBrains.Annotations;
using PDollarGestureRecognizer;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandRecognizer : MonoBehaviour
    {
        [Space]
        [Header("Configs")]
        [SerializeField] private float _minScoreMatch = 0.8f;
        
        [Space]
        [SerializeField] private List<TextAsset> _gestureTextAssets = new List<TextAsset>();

        [Space]
        [SerializeField] private List<SpellBase> _spells;
        
        private List<Gesture> _gestures = new List<Gesture>();

        private void Start()
        {
            foreach (var gestureTextAsset in _gestureTextAssets)
            {
                _gestures.Add(GestureIO.ReadGestureFromXML(gestureTextAsset.text));
            }
        }
        
        public bool TryRecognizeSpell(List<Point> points, out SpellBase targetSpell)
        {
            targetSpell = null;
            
            if (points.Count == 0)
            {
                return false;
            }
            
            Gesture candidate = new Gesture(points.ToArray());
            Result result = PointCloudRecognizer.Classify(candidate, _gestures.ToArray());

            if (result.Score < _minScoreMatch)
            {
                return false;
            }

            if (!TryFindSpellByGestureClass(result.GestureClass, out SpellBase spell))
            {
                Debug.LogError("Can't recognize spell!");
                return false;
            }

            targetSpell = spell;
            return true;
        }
        
        private bool TryFindSpellByGestureClass(string gestureClass, out SpellBase targetSpell)
        {
            targetSpell = null;
            
            foreach (var spell in _spells)
            {
                if (spell.GestureClass.Equals(gestureClass))
                {
                    targetSpell = spell;
                    return true;
                }
            }

            return false;
        }
    }
}