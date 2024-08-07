using HarryPoter.Core;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Chest : MonoBehaviour, ISpellable
{
    [SerializeField] private HandGrabInteractable _grabInteractable;
    
    public void OnOpenSpell()
    {
        Debug.Log("OPEN SPELL CHESS");
        _grabInteractable.enabled = true;
    }

    public void OnAttackSpell()
    {
    }

    public void OnTakeSpell()
    {
    }
}
