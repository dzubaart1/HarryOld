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
        _questHolder.QuestHolderCompleteEvent += OnQuestHolderComplete;
    }

    private void OnDisable()
    {
        _questHolder.QuestHolderCompleteEvent -= OnQuestHolderComplete;
    }

    public void OnQuestHolderComplete()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            return;
        }

        if (gameManager.CurrentLocalManager == null)
        {
            return;
        }

        //gameManager.CurrentLocalManager.TeleportGrabInteractableToPlayer(_ear);
        
        Destroy(gameObject);
    }
}
