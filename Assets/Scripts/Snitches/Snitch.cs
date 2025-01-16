using System;
using HarryPoter.Core;
using HarryPoter.Core.Quests;
using UnityEngine;

public class Snitch : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private QuestHolder _questHolder;
    
    private PlayerMovement _player;

    private TeleportManager _teleportManager;

    private void OnEnable()
    {
        _questHolder.QuestHolderCompletedEvent += OnQuestHolderCompleted;
    }

    private void OnDisable()
    {
        _questHolder.QuestHolderCompletedEvent -= OnQuestHolderCompleted;
    }

    public void OnQuestHolderCompleted()
    {
        gameObject.SetActive(false);
    }
}
