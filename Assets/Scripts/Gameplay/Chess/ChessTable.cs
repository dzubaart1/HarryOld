using Gameplay.Quests;
using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace HarryPoter.Core
{
    public class ChessTable : MonoBehaviour
    {
        [SerializeField] private Transform _rookPos;
        [SerializeField] private TriggerQuest _triggerQuest;

        private void OnEnable()
        {
            _triggerQuest.OnRookEnterEvent += OnRookEnter;
        }

        private void OnDisable()
        {
            _triggerQuest.OnRookEnterEvent -= OnRookEnter;
        }

        private void OnRookEnter(Transform rook)
        {
            rook.GetComponentInChildren<Collider>().enabled = false;
            rook.GetComponentInChildren<HandGrabInteractable>().enabled = false;
            
            rook.position = _rookPos.position;
            rook.rotation = _rookPos.rotation;
        }
    }
}