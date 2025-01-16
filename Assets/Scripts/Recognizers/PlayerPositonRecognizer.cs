using System;
using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;

namespace Mechaincs
{
    [RequireComponent(typeof(Collider))]
    public class PlayerPositionRecognizer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private QuestHolder _questHolder;

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponentInParent<Player>();

            if (player == null)
            {
                return;
            }

            if (!_questHolder.TryCompletePlayerPositionQuest(this))
            {
                return;
            }

            _meshRenderer.enabled = false;
        }

        private void OnTriggerStay(Collider other)
        {
            Player player = other.GetComponentInParent<Player>();

            if (player == null)
            {
                return;
            }

            if (!_questHolder.TryCompletePlayerPositionQuest(this))
            {
                return;
            }

            _meshRenderer.enabled = false;
        }
    }
}