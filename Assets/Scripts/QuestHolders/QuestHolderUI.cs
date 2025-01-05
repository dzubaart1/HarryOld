using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HarryPoter.Core.Quests
{
    public class QuestHolderUI : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private QuestHolder _questHolder;

        [Space]
        [Header("UIs")]
        [SerializeField] private RectTransform _questPanel;
        [SerializeField] private TMP_Text _questTitle;
        [SerializeField] private RectTransform _completePanel;
        
        private void Update()
        {
            if (_questHolder.IsComplete || _questHolder.CurrentQuest == null)
            {
                _completePanel.gameObject.SetActive(true);
                _questPanel.gameObject.SetActive(false);
            }
            else
            {
                _questTitle.text = _questHolder.CurrentQuest.QuestTitle;
                _completePanel.gameObject.SetActive(false);
                _questPanel.gameObject.SetActive(true);
            }
        }
    }
}