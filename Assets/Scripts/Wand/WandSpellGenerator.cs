using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandSpellGenerator : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private bool _isOn;
        [SerializeField] private string _spellName;

        public bool IsOn => _isOn;
        
        public void WriteNewSpell(List<Point> points)
        {
            string targetFileName = GenerateFileName();

            while (File.Exists(targetFileName))
            {
                targetFileName = GenerateFileName();
            }

            GestureIO.WriteGesture(points.ToArray(), _spellName,
                Application.persistentDataPath + targetFileName);
            
            Debug.Log("Create spell: " + Application.persistentDataPath + targetFileName);
        }

        private string GenerateFileName()
        {
            return $"{_spellName}_{Random.Range(0, 1000)}.xml";
        }
    }
}