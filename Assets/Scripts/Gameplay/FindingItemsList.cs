using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class FindingItemsList : MonoBehaviour
    {
        [Serializable]
        private struct ItemList
        {
            public GameObject GameObject;
            public FindingItemsService.EListItem ListItemType;
        }

        [SerializeField] private List<ItemList> _list;

        private FindingItemsService _findingItemsService;

        private void Awake()
        {
            _findingItemsService = Engine.GetService<FindingItemsService>();
        }

        private void OnEnable()
        {
            _findingItemsService.CheckInListItemEvent += OnCheckInListItem;
        }

        private void OnDisable()
        {
            _findingItemsService.CheckInListItemEvent -= OnCheckInListItem;
        }

        private void OnCheckInListItem(FindingItemsService.EListItem listItem)
        {
            ItemList res = GetListItemByType(listItem);
            res.GameObject.SetActive(true);
            
            Debug.Log("CHECK IN " + listItem);
        }

        private ItemList GetListItemByType(FindingItemsService.EListItem type)
        {
            foreach (var item in _list)
            {
                if (item.ListItemType == type)
                {
                    return item;
                }
            }

            throw new ArgumentException("Can't find List Item!");
        }
    }
}