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
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }

            Game game = gameManager.Game;
            
            if (game == null)
            {
                return;
            }

            foreach (var config in _itemListConfigs)
            {
                config.RectTransform.gameObject.SetActive(game.CompletedListItems.Contains(config.ListItemType));
            }
        }
    }
}