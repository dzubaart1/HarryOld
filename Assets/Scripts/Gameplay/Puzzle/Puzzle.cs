using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Puzzle : MonoBehaviour
    {
        [SerializeField] private List<PuzzlePart> _puzzleParts;

        private int _puzzleItems;

        private void Awake()
        {
            _puzzleItems = (1 << _puzzleParts.Count) - 1;
        }

        private void OnEnable()
        {
            foreach (var part in _puzzleParts)
            {
                part.PutPuzzleItemEvent += OnPutPuzzleItem;
            }
        }
        
        private void OnDisable()
        {
            foreach (var part in _puzzleParts)
            {
                part.PutPuzzleItemEvent -= OnPutPuzzleItem;
            }
        }
        private void OnPutPuzzleItem(PuzzleItem.EPuzzleItem puzzleItemType)
        {
            _puzzleItems ^= 1 << (int)puzzleItemType;

            if (_puzzleItems == 0)
            {
                CompletePuzzle();
            }
        }

        private void CompletePuzzle()
        {
            Debug.Log("Complete puzzle!");
        }
    }
}