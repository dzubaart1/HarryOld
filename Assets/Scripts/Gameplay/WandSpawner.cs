using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandSpawner : MonoBehaviour
    {
        private const float WAND_SPAWN_OFFSET = 0.3f;
        
        [SerializeField] private Wand _wandPrefab;

        private WandService _wandService;
        private Transform _centerEye;
        private List<PlayerGestures.GestureConfig> _wandGestures;
        
        private void Awake()
        {
            _wandService = Engine.GetService<WandService>();

            Player player = Engine.GetService<InputService>().Player;
            _centerEye = player.CenterEye;
            _wandGestures = player.PlayerGestures.GetConfigsByType(PlayerGestures.EGesture.Wand);
        }

        private void OnEnable()
        {
            foreach (var gesture in _wandGestures)
            {
                gesture.Selector.WhenSelected += SpawnWand;
            }
        }

        private void OnDisable()
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
                _wandService.CurrentWand.transform.position = CalcWandSpawnPoint();
                return;
            }

            Wand wand = Engine.Instantiate(_wandPrefab);
            wand.transform.position = CalcWandSpawnPoint();
            wand.transform.rotation = Quaternion.LookRotation(Vector3.up);
            
            _wandService.SetWand(wand);
        }

        private Vector3 CalcWandSpawnPoint()
        {
            return _centerEye.position + _centerEye.forward * WAND_SPAWN_OFFSET + _centerEye.transform.up;
        }
    }
}