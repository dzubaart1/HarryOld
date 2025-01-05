using System;
using System.Collections;
using HarryPoter.Core.LocalManagers.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HarryPoter.Core
{
    public class GameManager : MonoBehaviour
    {
        public event Action<string> LoadSceneCompleteEvent;
        
        [Header("Scenes")]
        [SerializeField] private string _firstFloorSceneName = "FirstFloorScene";
        [SerializeField] private string _secondFloorSceneName = "SecondFloorScene";

        [Space]
        [Header("Refs")]
        [SerializeField] private Game _game;
        
        public string FirstFloorSceneName => _firstFloorSceneName;
        public string SecondFloorSceneName => _secondFloorSceneName;

        public Game Game => _game;
        [CanBeNull] public static GameManager Instance { get; private set; }
        [CanBeNull] public BaseLocalManager CurrentLocalManager { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);
            
            while (!loadingScene.isDone)
            {
                yield return null;
            }
            
            LoadSceneCompleteEvent?.Invoke(sceneName);
        }
    }
}