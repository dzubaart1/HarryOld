using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;

        [CanBeNull] public Player Player { get; private set; }
        
        public void SpawnPlayer()
        {
            Player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}