using HarryPoter.Core;
using Oculus.Interaction;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Grabbable _grabbable;
    [SerializeField] private Quest _quest;

    private void OnEnable()
    {
        _grabbable.WhenPointerEventRaised += OnSelected;
    }

    private void OnDisable()
    {
        _grabbable.WhenPointerEventRaised -= OnSelected;
    }

    private void OnSelected(PointerEvent e)
    {
        _quest.Complete();
    }
}
