using UnityEngine;

namespace HarryPoter.Core
{
    public class Player : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Transform _head;
        [SerializeField] private PlayerMovement _playerMovement;

        [Header("Configs")]
        [SerializeField] private float _moveOffset = 1f;

        public Transform Head => _head;

        public void MoveForward()
        {
            Vector3 moveTarget = (_head.forward * _moveOffset) + transform.position;
            
            _playerMovement.UpdateMoveTarget(moveTarget);
            _playerMovement.ChangePositioning(true);
        }

        public void StopMoveForward()
        {
            _playerMovement.ChangePositioning(false);
        }
    }
}