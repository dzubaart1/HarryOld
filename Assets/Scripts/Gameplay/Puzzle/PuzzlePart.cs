using System;
using HarryPoter.Core;
using UnityEngine;

public class PuzzlePart : MonoBehaviour
{
    public event Action<PuzzleItem.EPuzzleItem> PutPuzzleItemEvent;
    
    [SerializeField] private PuzzleItem.EPuzzleItem _puzzleItemType;
    [SerializeField] private Transform _itemPos;
    
    private void OnTriggerEnter(Collider other)
    {
        PuzzleItem puzzleItem = other.GetComponent<PuzzleItem>();

        if (puzzleItem == null || puzzleItem.PuzzleItemType != _puzzleItemType)
        {
            return;
        }

        Debug.Log("Puzzle Part Trigger Enter");

        puzzleItem.gameObject.transform.position = _itemPos.position;
        puzzleItem.gameObject.transform.rotation = Quaternion.identity;
        puzzleItem.Grabbable.enabled = false;
        puzzleItem.Collider.enabled = false;
        
        PutPuzzleItemEvent?.Invoke(puzzleItem.PuzzleItemType);
    }
}
