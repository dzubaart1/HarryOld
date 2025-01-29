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
        
        private void Update()
        {
            if (_questHolder.IsComplete || _questHolder.CurrentQuest == null)
            {
                _questPanel.gameObject.SetActive(false);
            }
            else
            {
                _questTitle.text = _questHolder.CurrentQuest.QuestTitle;
                _questPanel.gameObject.SetActive(true);
            }
        }
    }
}