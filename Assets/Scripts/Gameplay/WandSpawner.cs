using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandSpawner : MonoBehaviour
    {
        private const float WAND_SPAWN_OFFSET = 0.5f;
        
        [SerializeField] private Wand _wandPrefab;

        private WandService _wandService;
        private Transform _player;
        private List<GestureConfiguration.GestureConfig> _wandGestures;
        
        private void Awake()
        {
            _wandService = Engine.GetService<WandService>();
            _player = Engine.GetService<InputService>().Player.transform;
            
            _wandGestures = Engine.GetService<GestureService>().GetConfigsByType(GestureService.EGesture.Wand);

            foreach (var gesture in _wandGestures)
            {
                gesture.Selector.WhenSelected += SpawnWand;
            }
        }

        private void OnDestroy()
        {
            foreach (var gesture in _wandGestures)
            {
                gesture.Selector.WhenSelected -= SpawnWand;
            }
        }

        private void SpawnWand()
        {
            if (_wandService.CurrentWand != null)
            {
                Debug.Log("Teleport Wand!");
                _wandService.CurrentWand.transform.position = CalcWandSpawnPoint();
                return;
            }
            
            Debug.Log("Spawn Wand!");
            Wand wand = Engine.Instantiate(_wandPrefab);
            wand.transform.position = CalcWandSpawnPoint();
            _wandService.SetWand(wand);
        }

        private Vector3 CalcWandSpawnPoint()
        {
            return _player.position + _player.forward * WAND_SPAWN_OFFSET + _player.transform.up;
        }
    }
}