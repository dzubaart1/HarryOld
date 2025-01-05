using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace HarryPoter.Core
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;

        [CanBeNull] public Player Player { get; private set; }
        
        public void SpawnPlayerOnScene()
        {
            Vector3 spawnPos = Vector3.zero;
            
            PlayerSpawnPoint playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();

            if (playerSpawnPoint != null)
            {
                spawnPos = playerSpawnPoint.transform.position;
            }

            if (Player == null)
            {
                Player = Instantiate(_playerPrefab, spawnPos, Quaternion.identity, transform);   
            }
            else
            {
                Player.transform.position = spawnPos;
                Player.transform.rotation = Quaternion.identity;
            }
        }
    }
}