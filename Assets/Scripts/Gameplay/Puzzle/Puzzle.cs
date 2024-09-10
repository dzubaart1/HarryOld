using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Puzzle : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private List<PuzzlePart> _puzzleParts;
        [SerializeField] private GameObject _top;
        [SerializeField] private GameObject _gift;

        private ParticlesService _particlesService;
        
        private int _puzzleItems;

        private void Awake()
        {
            _puzzleItems = (1 << _puzzleParts.Count) - 1;
            _gift.gameObject.SetActive(false);

            _particlesService = Engine.GetService<ParticlesService>();
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
            _particlesService.SpawnParticlesSystem(ParticlesConfiguration.EParticle.QuestComplete, transform.position).Play();
            
            _top.gameObject.SetActive(false);
            _gift.gameObject.SetActive(true);
            foreach (var puzzlePart in _puzzleParts)
            {
                puzzlePart.HidePawn();
            }
        }
    }
}