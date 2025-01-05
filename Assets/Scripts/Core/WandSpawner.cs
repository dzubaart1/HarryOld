using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandSpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Wand _wandPrefab;
        
        [CanBeNull] public Wand Wand { get; private set; }

        public void SpawnWand()
        {
            if (Wand == null)
            {
                Wand = Instantiate(_wandPrefab, Vector3.zero, Quaternion.identity);   
            }
        }
    }
}