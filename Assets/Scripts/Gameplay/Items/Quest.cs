using System;
using UnityEngine;

namespace HarryPoter.Core
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] private GameObject _gift;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            _gift.SetActive(false);
        }

        public void Complete()
        {
            _gift.SetActive(true);
            _particleSystem.Play();
        }
    }
}