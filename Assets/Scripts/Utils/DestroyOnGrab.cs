using System.Collections;
using HarryPoter.Core;
using UnityEngine;

namespace Utils
{
    public class DestroyOnGrab : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private HandGrabInteractableCollector _grabInteractable;
        
        [Space]
        [Header("Configs")]
        [SerializeField] private float _disappearDelay = 3f;
        
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

            StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(_disappearDelay);
            
            ParticlesManager particlesManager = ParticlesManager.Instance;
            if (particlesManager == null)
            {
                yield break;
            }
            
            if(!particlesManager.TryGetParticlesSystem(ParticlesManager.EParticle.DisapearItemEffect, out ParticleSystem disaperPS))
            {
                yield break;
            }

            disaperPS.transform.position = transform.position;
            disaperPS.Play();
            
            Destroy(gameObject);
        }
    }
}