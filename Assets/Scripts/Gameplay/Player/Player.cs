using UnityEngine;

namespace HarryPoter.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        public Transform SpawnPoint => _spawnPoint;
    }
}