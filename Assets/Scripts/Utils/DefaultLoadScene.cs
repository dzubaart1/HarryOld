using HarryPoter.Core;
using UnityEngine;

namespace Utils
{
    public class DefaultLoadScene : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private GameManager _gameManager;

        [Space]
        [Header("Configs")]
        [SerializeField] private string _sceneName;
        
        private void Start()
        {
            _gameManager.LoadScene(_sceneName);
        }
    }
}