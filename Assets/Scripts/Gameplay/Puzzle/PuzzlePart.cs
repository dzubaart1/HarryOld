using System;
using HarryPoter.Core;
using UnityEngine;

public class PuzzlePart : MonoBehaviour
{
    public event Action<PuzzleItem.EPuzzleItem> PutPuzzleItemEvent;
    
    [SerializeField] private PuzzleItem.EPuzzleItem _puzzleItemType;
    [SerializeField] private Transform _itemPos;

    private PuzzleItem _currentItem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_currentItem != null)
        {
            return;
        }
        
        PuzzleItem puzzleItem = other.GetComponent<PuzzleItem>();

        if (puzzleItem == null || puzzleItem.PuzzleItemType != _puzzleItemType)
        {
            return;
        }

        _currentItem = puzzleItem;

        _currentItem.GrabInteractable.ToggleGrabbing(false);
        _currentItem.gameObject.transform.position = _itemPos.position;
        _currentItem.gameObject.transform.rotation = Quaternion.identity;
        _currentItem.Collider.enabled = false;
        
        PutPuzzleItemEvent?.Invoke(_currentItem.PuzzleItemType);
    }

    public void HidePawn()
    {
        if (_currentItem == null)
        {
            return;
        }
        
        _currentItem.gameObject.SetActive(false);
    }
}
