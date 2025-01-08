using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class TargetItemsList : MonoBehaviour
    {
        [Serializable]
        private class ItemListConfig
        {
            public RectTransform RectTransform;
            public EListItem ListItemType;
        }

        [SerializeField] private List<ItemListConfig> _itemListConfigs;

        [CanBeNull] private Game _game;

        private void Update()
        {
            if (_game == null)
            {
                return;
            }

            foreach (var config in _itemListConfigs)
            {
                config.RectTransform.gameObject.SetActive(_game.CompletedListItems.Contains(config.ListItemType));
            }
        }
        
        public void InitTargetItemsList(Game game)
        {
            _game = game;
        }
    }
}