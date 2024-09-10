using Oculus.Interaction;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PuzzleItem : MonoBehaviour, ISpellable
    {
        public enum EPuzzleItem
        {
            Red,
            Green,
            Yellow,
            Blue
        }

        public EPuzzleItem PuzzleItemType
        {
            get
            {
                return _puzzleItemType;
            }
        }

        public GrabInteractable GrabInteractable
        {
            get
            {
                return _grabInteractable;
            }
        }

        public Collider Collider
        {
            get
            {
                return _collider;
            }
        }

        [SerializeField] private EPuzzleItem _puzzleItemType;
        [SerializeField] private GrabInteractable _grabInteractable;
        [SerializeField] private Collider _collider;

        private TeleportService _teleportService;
        
        private Transform _spawnPoint;
        
        private void Awake()
        {
            _teleportService = Engine.GetService<TeleportService>();
            _spawnPoint = Engine.GetService<InputService>().Player.SpawnPoint;
        }

        public void OnOpenSpell()
        {
        }

        public void OnAttackSpell()
        {
        }

        public void OnTakeSpell()
        {
            _teleportService.Teleport(_grabInteractable, _spawnPoint);
        }
    }
}