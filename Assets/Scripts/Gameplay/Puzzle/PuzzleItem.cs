using System;
using Oculus.Interaction;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PuzzleItem : MonoBehaviour
    {
        public enum EPuzzleItem
        {
            Red,
            Green,
            Yellow,
            Blue
        }

        public EPuzzleItem PuzzleItemType
        {
            get
            {
                return _puzzleItemType;
            }
        }

        public Grabbable Grabbable
        {
            get
            {
                return _grabbable;
            }
        }

        public Collider Collider
        {
            get
            {
                return _collider;
            }
        }

        [SerializeField] private EPuzzleItem _puzzleItemType;
        [SerializeField] private Grabbable _grabbable;
        [SerializeField] private Collider _collider;
    }
}