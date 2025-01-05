using UnityEngine;
using UnityEngine.Serialization;

namespace HarryPoter.Core
{
    public class Player : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Transform _head;
        [SerializeField] private PlayerVoiceRecognizer _playerVoiceRecognizer;
        [SerializeField] private PlayerObjectTeleport _playerObjectTeleport;
        [SerializeField] private PlayerMovement _playerMovement;

        [Header("Configs")]
        [SerializeField] private float _moveOffset;

        public Transform Head => _head;

        public void MoveForward()
        {
            Vector3 moveTarget = (_head.forward - transform.position).normalized * _moveOffset;
            moveTarget.y = 0;
            
            _playerMovement.UpdateMoveTarget(moveTarget);
            _playerMovement.ChangePositioning(true);
        }

        public void StopMoveForward()
        {
            _playerMovement.ChangePositioning(false);
        }

        public void TeleportGrabInteractableToPlayer(GrabInteractable obj)
        {
            _playerObjectTeleport.Teleport(obj);
        }

        public void StartVoiceRecording()
        {
            _playerVoiceRecognizer.StartVoiceRecognition();
        }
    }
}