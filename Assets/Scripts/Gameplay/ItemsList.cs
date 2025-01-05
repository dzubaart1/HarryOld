using System;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPoter.Core
{
    public class ItemsList : MonoBehaviour
    {
        [Serializable]
        private class ItemListConfig
        {
            public RectTransform RectTransform;
            public EListItem ListItemType;
        }

        [SerializeField] private List<ItemListConfig> _itemListConfigs;
        
        public void OnQuestHolderComplete(EListItem listItem)
        {
            ItemListConfig res = GetListItemByType(listItem);

            if (TryGetItemListConfig(listItem, out ItemListConfig itemListConfig))
            {
                itemListConfig.RectTransform.gameObject.SetActive(true);
            }
        }

        private bool TryGetItemListConfig(EListItem type, out ItemListConfig config)
        {
            config = null;
            
            foreach (var itemListConfig in _itemListConfigs)
            {
                if (itemListConfig.ListItemType == type)
                {
                    config = itemListConfig;
                    return true;
                }
            }
            return false;
        }
        
        private ItemListConfig GetListItemByType(EListItem type)
        {


            throw new ArgumentException("Can't find List Item!");
        }
    }
}