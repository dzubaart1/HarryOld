using UnityEngine;

namespace HarryPoter.Core
{
    public class PuzzleItem : MonoBehaviour
    {
        /*public enum EPuzzleItem
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

        private PlayerObjectTeleport _playerObjectTeleport;

        private PlayerMovement _player;
        
        private void Awake()
        {
            _playerObjectTeleport = Engine.GetService<PlayerObjectTeleport>();
            _player = Engine.GetService<InputService>().Player;
        }

        public void OnOpenSpell()
        {
        }

        public void OnAttackSpell()
        {
        }

        public void OnTakeSpell()
        {
            _playerObjectTeleport.Teleport(_grabInteractable, _player.SpawnPoint, Quaternion.identity);
        }*/
    }
}