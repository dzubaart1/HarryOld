using System;
using HarryPoter.Core;
using UnityEngine;

namespace Utils
{
    public class DefaultLoadScene : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private void Start()
        {
            _gameManager.LoadScene(_gameManager.FirstFloorSceneName);
        }
    }
}