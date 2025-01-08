using System.Collections;
using UnityEngine;

namespace HarryPoter.Core
{
    public class TargetItem : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private HandGrabInteractableCollector _grabInteractable;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _disappearDelay = 3f;
        [SerializeField] private EListItem _listItem;

        public HandGrabInteractableCollector HandGrabInteractableCollector => _grabInteractable;
        
        private void OnEnable()
        {
            _grabInteractable.GrabEvent += OnGrab;
        }

        private void OnDisable()
        {
            _grabInteractable.GrabEvent -= OnGrab;
        }

        private void OnGrab()
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                return;
            }
            
            gameManager.Game.OnTargetItemPickedUp(_listItem);
            StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(_disappearDelay);
            gameObject.SetActive(false);
        }
    }
}