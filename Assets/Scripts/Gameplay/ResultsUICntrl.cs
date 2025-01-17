using System.Globalization;
using TMPro;
using UnityEngine;

namespace HarryPoter.Core
{
    public class ResultsUICntrl : MonoBehaviour
    {
        [SerializeField] private HandGrabInteractableCollector _grabInteractableCollector;
        [SerializeField] private TMP_Text _gameTimeText;

        public HandGrabInteractableCollector GrabInteractableCollector => _grabInteractableCollector;

        public void SetGameTime(float gameTime)
        {
            _gameTimeText.text = gameTime.ToString(CultureInfo.CurrentCulture) + " сек";
        }
    }
}